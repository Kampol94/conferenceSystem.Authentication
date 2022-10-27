using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using UserService.API.Controllers;
using UserService.Application.UserRegistrations.ConfirmUserRegistration;
using UserService.Application.UserRegistrations.RegisterNewUser;

namespace CompanyName.MyMeetings.API.Modules.UserAccess;

[ApiController]
public class UserRegistrationsController : BaseApiController
{
    [AllowAnonymous]
    [HttpPost("")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<IActionResult> RegisterNewUser(RegisterNewUserCommand request)
    {
        return Ok(await Mediator.Send(request));
    }

    [AllowAnonymous]
    [HttpPatch("{userRegistrationId}/confirm")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<IActionResult> ConfirmRegistration([FromRoute] Guid userRegistrationId)
    {
        return Ok(await Mediator.Send(new ConfirmUserRegistrationCommand(userRegistrationId)));
    }
}