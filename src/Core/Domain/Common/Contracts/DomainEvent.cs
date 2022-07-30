using AuthenticationService.Shared.Events;

namespace AuthenticationService.Domain.Common.Contracts;

public abstract class DomainEvent : IEvent
{
    public DateTime TriggeredOn { get; protected set; } = DateTime.UtcNow;
}