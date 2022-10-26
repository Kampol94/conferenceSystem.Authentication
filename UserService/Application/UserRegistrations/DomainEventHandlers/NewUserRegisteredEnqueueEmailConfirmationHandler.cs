using MediatR;
using UserService.Application.UserRegistrations.SendUserRegistrationConfirmationEmail;
using UserService.Domain.UserRegistrations.Events;

namespace UserService.Application.UserRegistrations.DomainEventHandlers;

public class NewUserRegisteredEnqueueEmailConfirmationHandler : INotificationHandler<NewUserRegisteredDomainEvent>
{
    private readonly IMediator _mediator;

    public NewUserRegisteredEnqueueEmailConfirmationHandler(IMediator mediator)
    {
        _mediator = mediator;
    }

    public async Task Handle(NewUserRegisteredDomainEvent @event, CancellationToken cancellationToken)
    {
        await _mediator.Send(new SendUserRegistrationConfirmationEmailCommand(
            Guid.NewGuid(),
            @event.UserRegistrationId,
            @event.Email,
            @event.ConfirmLink), cancellationToken);
    }
}