using MediatR;
using UserService.Application.Contracts;
using UserService.Application.IntegrationEvents.Events;
using UserService.Domain.UserRegistrations.Events;

namespace UserService.Application.UserRegistrations.DomainEventHandlers;

public class NewUserRegisteredPublishEventHandler : INotificationHandler<NewUserRegisteredDomainEvent>
{
    private readonly IEventsBus _eventsBus;

    public NewUserRegisteredPublishEventHandler(IEventsBus eventsBus)
    {
        _eventsBus = eventsBus;
    }

    public async Task Handle(NewUserRegisteredDomainEvent @event, CancellationToken cancellationToken)
    {
        await _eventsBus.Publish(new NewUserRegisteredIntegrationEvent(
            @event.Id,
            @event.When,
            @event.UserRegistrationId.Value,
            @event.Login,
            @event.Email,
            @event.FirstName,
            @event.LastName,
            @event.Name));
    }
}