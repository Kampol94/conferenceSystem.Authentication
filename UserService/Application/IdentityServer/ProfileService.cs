using IdentityServer4.Models;
using IdentityServer4.Services;

namespace UserService.Application.IdentityServer;

public class ProfileService : IProfileService
{
    public Task GetProfileDataAsync(ProfileDataRequestContext context)
    {
        context.IssuedClaims.AddRange(context.Subject.Claims.Where(x => x.Type == ClaimTypes.Roles).ToList());
        context.IssuedClaims.Add(context.Subject.Claims.Single(x => x.Type == ClaimTypes.Name));
        context.IssuedClaims.Add(context.Subject.Claims.Single(x => x.Type == ClaimTypes.Email));

        return Task.CompletedTask;
    }

    public Task IsActiveAsync(IsActiveContext context)
    {
        return Task.FromResult(context.IsActive);
    }
}