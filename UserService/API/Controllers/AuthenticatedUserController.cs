using Microsoft.AspNetCore.Mvc;
using UserService.Application.Authorization.GetAuthenticatedUserPermissions;
using UserService.Application.Authorization.GetUserPermissions;
using UserService.Application.Users.GetAuthenticatedUser;
using UserService.Application.Users.GetUser;

namespace UserService.API.Controllers;

[ApiController]
public class AuthenticatedUserController : BaseApiController
{
    [HttpGet("")]
    [ProducesResponseType(typeof(UserDto), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetAuthenticatedUser()
    {
        return Ok(await Mediator.Send(new GetAuthenticatedUserQuery()));
    }

    [HttpGet("permissions")]
    [ProducesResponseType(typeof(List<UserPermissionDto>), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetAuthenticatedUserPermissions()
    {
        return Ok(await Mediator.Send(new GetAuthenticatedUserPermissionsQuery()));
    }
}