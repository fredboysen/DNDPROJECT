using BookTradingHub.WebApp.Components;
using Blazored.LocalStorage;
using BookTradingHub.WebAPI.Auth;
using BookTradingHub.WebApp.Services.Http;
using Microsoft.AspNetCore.Components.Authorization;
using System.Security.Claims;


var builder = WebApplication.CreateBuilder(args);

builder.Services.AddHttpClient("WebAPI", client =>
{
    client.BaseAddress = new Uri("https://localhost:7167");  
});


builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri("https://localhost:7167") });

builder.Services.AddAuthorizationCore(options =>
{
    options.AddPolicy("Admin", policy =>
        policy.RequireAuthenticatedUser().RequireClaim(ClaimTypes.Role, "Admin"));
});
builder.Services.AddAuthentication().AddCookie(options =>
{
    options.LoginPath = "/login";
});

builder.Services.AddScoped<IAuthService, JwtAuthService>();
builder.Services.AddScoped<AuthenticationStateProvider, CustomAuthProvider>();
AuthorizationPolicies.AddPolicies(builder.Services);


builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents();
builder.Services.AddBlazoredLocalStorage();

var app = builder.Build();


if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error"); 
    app.UseHsts();
}

app.UseAuthorization();

app.UseHttpsRedirection();
app.UseStaticFiles(); 
app.UseAntiforgery();
app.MapStaticAssets(); 
app.MapRazorComponents<App>()
    .AddInteractiveServerRenderMode(); 


app.Run();
