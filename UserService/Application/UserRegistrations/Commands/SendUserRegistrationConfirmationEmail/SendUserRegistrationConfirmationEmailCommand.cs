using UserService.Application.Contracts.Commands;
using UserService.Domain.UserRegistrations;

namespace UserService.Application.UserRegistrations.SendUserRegistrationConfirmationEmail;

public class SendUserRegistrationConfirmationEmailCommand : CommandBase
{
    public SendUserRegistrationConfirmationEmailCommand(
        Guid id,
        UserRegistrationId userRegistrationId,
        string email,
        string confirmLink)
        : base(id)
    {
        UserRegistrationId = userRegistrationId;
        Email = email;
        ConfirmLink = confirmLink;
    }

    internal UserRegistrationId UserRegistrationId { get; }

    internal string Email { get; }

    internal string ConfirmLink { get; }
}