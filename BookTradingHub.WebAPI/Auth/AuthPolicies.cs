using System.Security.Claims;
using Microsoft.Extensions.DependencyInjection;

namespace BookTradingHub.WebAPI.Auth;
public static class AuthorizationPolicies
{
    public static void AddPolicies(IServiceCollection services)
    {
        services.AddAuthorizationCore(options =>
        {
            options.AddPolicy("User", a =>
                a.RequireAuthenticatedUser().RequireClaim("Role", "User"));

            options.AddPolicy("Admin", a =>
                a.RequireAuthenticatedUser().RequireClaim("Role", "Admin"));
        });
    }
}
