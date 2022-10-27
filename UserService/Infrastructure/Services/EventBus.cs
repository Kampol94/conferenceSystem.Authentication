using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UserService.Application.Contracts;

namespace UserService.Infrastructure.Services;
internal class EventBus : IEventsBus
{
    public Task Publish<T>(T @event) where T : IntegrationEvent
    {
        return Task.CompletedTask; 
    }

    public void StartConsuming()
    {
    }

    public void Subscribe<T>(IIntegrationEventHandler<T> handler) where T : IntegrationEvent
    {
    }
}
