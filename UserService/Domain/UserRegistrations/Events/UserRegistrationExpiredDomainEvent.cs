using UserService.Domain.Contracts;

namespace UserService.Domain.UserRegistrations.Events;

public class UserRegistrationExpiredDomainEvent : DomainEventBase
{
    public UserRegistrationExpiredDomainEvent(UserRegistrationId userRegistrationId)
    {
        UserRegistrationId = userRegistrationId;
    }

    public UserRegistrationId UserRegistrationId { get; }
}