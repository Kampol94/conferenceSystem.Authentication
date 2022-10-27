using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using UserService.Application.Authentication.Authenticate;

namespace UserService.API.Controllers;

[ApiController]
public class UsersController : BaseApiController
{
    [HttpPost("authenticate")]
    [AllowAnonymous]
    public async Task<IActionResult> Authenticate([FromBody] AuthenticateCommand command)
    {
        var result = await Mediator.Send(command);

        if (result.IsAuthenticated)
        {
            return Ok(result);
        }

        return Unauthorized(result);
    }
}
