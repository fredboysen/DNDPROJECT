using System.Security.Claims;

namespace BookTradingHub.WebApp.Extensions
{
    public static class ClaimsPrincipalExtensions
    {
        public static bool IsInRole(this ClaimsPrincipal user, string role)
        {
            return user?.Claims.Any(c =>
                c.Type == ClaimTypes.Role &&
                c.Value.Equals(role, StringComparison.OrdinalIgnoreCase)) ?? false;
        }
    }
}
