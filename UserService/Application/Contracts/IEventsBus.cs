namespace UserService.Application.Contracts;

public interface IEventBus
{
    void Publish<T>(T @event)
            where T : IntegrationEvent;

    void Subscribe<U>(IServiceProvider services) where U : IntegrationEvent;
        
    void Consume();
}