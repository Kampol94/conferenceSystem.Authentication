﻿namespace UserService.Application.Contracts;

public interface IEventsBus
{
    void Publish<T>(T @event)
            where T : IntegrationEvent;

    void Subscribe<T>(IIntegrationEventHandler<T> handler)
        where T : IntegrationEvent;
}