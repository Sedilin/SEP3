using System.Security.Claims;
using Microsoft.Extensions.DependencyInjection;

namespace Shared.Auth;

public class AuthorizationPolicies
{
    public static void AddPolicies(IServiceCollection services)
    {
        services.AddAuthorizationCore(options =>
        {
            options.AddPolicy("MustBeUser", a =>
                a.RequireAuthenticatedUser().RequireClaim("Type", "User"));
            options.AddPolicy("MustBeTutor", a =>
                a.RequireAuthenticatedUser().RequireClaim("Type", "Tutor"));
        });
    }
}