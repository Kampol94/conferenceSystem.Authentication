using IdentityServer4.Models;
using IdentityServer4.Validation;
using MediatR;
using UserService.Application.Authentication.Authenticate;

namespace CompanyName.MyMeetings.API.Modules.UserAccess;

public class ResourceOwnerPasswordValidator : IResourceOwnerPasswordValidator
{
    private readonly IMediator _mediator;

    public ResourceOwnerPasswordValidator(IMediator mediator)
    {
        _mediator = mediator;
    }

    public async Task ValidateAsync(ResourceOwnerPasswordValidationContext context)
    {
        var authenticationResult = await _mediator.Send(
            new AuthenticateCommand(context.UserName, context.Password));

        if (!authenticationResult.IsAuthenticated)
        {
            context.Result = new GrantValidationResult(
                TokenRequestErrors.InvalidGrant,
                authenticationResult.AuthenticationError);

            return;
        }

        context.Result = new GrantValidationResult(
            authenticationResult.User.Id.ToString(),
            "forms",
            authenticationResult.User.Claims);
    }
}