using Microsoft.Extensions.Hosting;
using UserService.Application.Contracts;

namespace UserService.Infrastructure.Services;

public class EventReceiverService : IHostedService
{
    private readonly IEventsBus _eventBus;
    private Timer? _timer = null;

    public EventReceiverService(IEventsBus eventBus)
    {
        _eventBus = eventBus;
    }

    public Task StartAsync(CancellationToken cancellationToken)
    {

        _timer = new Timer(DoWork, null, TimeSpan.Zero,
            TimeSpan.FromSeconds(5));

        return Task.CompletedTask;
    }

    private void DoWork(object? state)
    {
        _eventBus.Consume();
    }

    public Task StopAsync(CancellationToken cancellationToken)
    {
        return Task.CompletedTask;
    }
}
