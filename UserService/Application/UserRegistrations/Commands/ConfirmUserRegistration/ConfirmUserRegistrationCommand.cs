using UserService.Application.Contracts.Commands;

namespace UserService.Application.UserRegistrations.ConfirmUserRegistration;

public class ConfirmUserRegistrationCommand : CommandBase
{
    public ConfirmUserRegistrationCommand(Guid userRegistrationId)
    {
        UserRegistrationId = userRegistrationId;
    }

    public Guid UserRegistrationId { get; }
}