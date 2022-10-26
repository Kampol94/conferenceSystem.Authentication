using UserService.Domain.Contracts;

namespace UserService.Domain.UserRegistrations.Events;

public class UserRegistrationConfirmedDomainEvent : DomainEventBase
{
    public UserRegistrationConfirmedDomainEvent(UserRegistrationId userRegistrationId)
    {
        UserRegistrationId = userRegistrationId;
    }

    public UserRegistrationId UserRegistrationId { get; }
}