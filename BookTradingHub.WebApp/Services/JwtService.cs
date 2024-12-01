using System.Security.Claims;
using System.Text;
using System.Text.Json;
using BookTradingHub.WebAPI.Models;
using Microsoft.JSInterop;

namespace BookTradingHub.WebApp.Services.Http
{
    public class JwtAuthService : IAuthService
    {
        private readonly HttpClient _client;
        private readonly IJSRuntime _jsRuntime;

        public string Jwt { get; private set; } = string.Empty;

        public Action<ClaimsPrincipal> OnAuthStateChanged { get; set; } = null!;

        public JwtAuthService(HttpClient client, IJSRuntime jsRuntime)
        {
            _client = client;
            _jsRuntime = jsRuntime;
        }

        public async Task LoginAsync(string username, string password)
        {
            var loginDTO = new LoginDTO
            {
                username = username,
                password = password
            };

            var userAsJson = JsonSerializer.Serialize(loginDTO);
            var content = new StringContent(userAsJson, Encoding.UTF8, "application/json");

            var response = await _client.PostAsync("/auth/login", content);
            var responseContent = await response.Content.ReadAsStringAsync();

            if (!response.IsSuccessStatusCode)
            {
                throw new Exception($"Login failed: {responseContent}");
            }

            Jwt = responseContent;
            await CacheTokenAsync();

            var principal = await CreateClaimsPrincipal();
            OnAuthStateChanged.Invoke(principal);
        }

        public async Task LogoutAsync()
        {
            await ClearTokenFromCacheAsync();
            Jwt = string.Empty;
            var principal = new ClaimsPrincipal();
            OnAuthStateChanged.Invoke(principal);
        }

        public async Task RegisterAsync(User user)
        {
            var userAsJson = JsonSerializer.Serialize(user);
            var content = new StringContent(userAsJson, Encoding.UTF8, "application/json");
            var response = await _client.PostAsync("/auth/register", content);
            var responseContent = await response.Content.ReadAsStringAsync();

            if (!response.IsSuccessStatusCode)
            {
                throw new Exception($"Registration failed: {responseContent}");
            }
        }

        public async Task<ClaimsPrincipal> GetAuthAsync()
        {
            var principal = await CreateClaimsPrincipal();
            return principal;
        }

        private async Task<ClaimsPrincipal> CreateClaimsPrincipal()
        {
            var cachedToken = await GetTokenFromCacheAsync();

            if (string.IsNullOrEmpty(Jwt) && string.IsNullOrEmpty(cachedToken))
            {
                return new ClaimsPrincipal();
            }

            if (!string.IsNullOrEmpty(cachedToken))
            {
                Jwt = cachedToken;
            }

            if (!_client.DefaultRequestHeaders.Contains("Authorization"))
            {
                _client.DefaultRequestHeaders.Add("Authorization", "Bearer " + Jwt);
            }

            var claims = ParseClaimsFromJwt(Jwt);
            var identity = new ClaimsIdentity(claims, "jwt");
            return new ClaimsPrincipal(identity);
        }

        private static IEnumerable<Claim> ParseClaimsFromJwt(string jwt)
        {
            var payload = jwt.Split('.')[1];
            var jsonBytes = ParseBase64WithoutPadding(payload);
            var keyValuePairs = JsonSerializer.Deserialize<Dictionary<string, object>>(jsonBytes);

            if (keyValuePairs == null)
                throw new Exception("Failed to parse claims from JWT.");

            return keyValuePairs.Select(kvp =>
            {
                var key = kvp.Key == "role" ? ClaimTypes.Role : kvp.Key;
                return new Claim(key, kvp.Value.ToString() ?? string.Empty);
            });
        }

        private static byte[] ParseBase64WithoutPadding(string base64)
        {
            switch (base64.Length % 4)
            {
                case 2: base64 += "=="; break;
                case 3: base64 += "="; break;
            }
            return Convert.FromBase64String(base64);
        }

        private async Task<string?> GetTokenFromCacheAsync()
        {
            return await _jsRuntime.InvokeAsync<string>("localStorage.getItem", "jwt");
        }

        private async Task CacheTokenAsync()
        {
            await _jsRuntime.InvokeVoidAsync("localStorage.setItem", "jwt", Jwt);
        }

        private async Task ClearTokenFromCacheAsync()
        {
            await _jsRuntime.InvokeVoidAsync("localStorage.removeItem", "jwt");
        }
    }
}
