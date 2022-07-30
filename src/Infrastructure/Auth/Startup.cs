using AuthenticationService.Application.Common.Interfaces;
using AuthenticationService.Infrastructure.Auth.AzureAd;
using AuthenticationService.Infrastructure.Auth.Jwt;
using AuthenticationService.Infrastructure.Identity;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace AuthenticationService.Infrastructure.Auth;

internal static class Startup
{
    internal static IServiceCollection AddAuth(this IServiceCollection services, IConfiguration config)
    {
        services
            .AddCurrentUser()

            // Must add identity before adding auth!
            .AddIdentity();
        services.Configure<SecuritySettings>(config.GetSection(nameof(SecuritySettings)));
        return config["SecuritySettings:Provider"].Equals("AzureAd", StringComparison.OrdinalIgnoreCase)
            ? services.AddAzureAdAuth(config)
            : services.AddJwtAuth(config);
    }

    internal static IApplicationBuilder UseCurrentUser(this IApplicationBuilder app) =>
        app.UseMiddleware<CurrentUserMiddleware>();

    private static IServiceCollection AddCurrentUser(this IServiceCollection services) =>
        services
            .AddScoped<CurrentUserMiddleware>()
            .AddScoped<ICurrentUser, CurrentUser>()
            .AddScoped(sp => (ICurrentUserInitializer)sp.GetRequiredService<ICurrentUser>());
}