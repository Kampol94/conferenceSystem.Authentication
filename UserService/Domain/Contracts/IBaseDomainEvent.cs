using MediatR;

namespace UserService.Domain.Contracts;

public interface IBaseDomainEvent : INotification
{
    Guid Id { get; }

    DateTime When { get; }
}