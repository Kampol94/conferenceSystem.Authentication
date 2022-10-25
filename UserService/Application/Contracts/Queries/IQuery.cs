using MediatR;

namespace UserService.Application.Contracts.Queries;

public interface IQuery<out TResult> : IRequest<TResult>
{
}