using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace UserService.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public abstract class BaseApiController : ControllerBase
{
    private ISender? _mediator;
    protected ISender Mediator => _mediator ??= HttpContext.RequestServices.GetService<ISender>() ?? throw new Exception();
}
