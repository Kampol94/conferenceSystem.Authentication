using MediatR;

namespace UserService.Application.Contracts.Commands;
public class CommandHandlerDecorator<T> : ICommandHandler<T> where T : ICommand
{
    private readonly ICommandHandler<T> _decorated;

    private readonly IRepository _repository;

    public CommandHandlerDecorator(ICommandHandler<T> decorated, IRepository repository)
    {
        _decorated = decorated;
        _repository = repository;
    }

    public async Task<Unit> Handle(T request, CancellationToken cancellationToken)
    {
        await _decorated.Handle(request, cancellationToken);

        await _repository.CommitAsync();

        return Unit.Value;
    }
}

public class CommandHandlerDecorator<TCommand, TResult> : ICommandHandler<TCommand, TResult> where TCommand : ICommand<TResult>
{
    private readonly ICommandHandler<TCommand, TResult> _decorated;

    private readonly IRepository _repository;

    public CommandHandlerDecorator(ICommandHandler<TCommand, TResult> decorated, IRepository repository)
    {
        _decorated = decorated;
        _repository = repository;
    }

    public async Task<TResult> Handle(TCommand request, CancellationToken cancellationToken)
    {
        var result = await _decorated.Handle(request, cancellationToken);

        await _repository.CommitAsync();

        return result;
    }
}
