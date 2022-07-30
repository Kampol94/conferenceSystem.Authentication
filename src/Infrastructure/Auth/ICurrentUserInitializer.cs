using System.Security.Claims;

namespace AuthenticationService.Infrastructure.Auth;

public interface ICurrentUserInitializer
{
    void SetCurrentUser(ClaimsPrincipal user);

    void SetCurrentUserId(string userId);
}