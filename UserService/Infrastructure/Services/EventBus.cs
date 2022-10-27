using IdentityServer4.Models;
using MediatR;
using Microsoft.AspNetCore.Connections;
using Newtonsoft.Json;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UserService.Application.Contracts;

namespace UserService.Infrastructure.Services;
public class EventBus : IEventsBus
{
    private readonly IModel _channel;
    private Dictionary<string, EventingBasicConsumer> _consumers = new();

    public EventBus(IMediator mediator)
    {
        ConnectionFactory factory = new()
        {
            HostName = "rabbitmq"
        };
        //Create the RabbitMQ connection using connection factory details as i mentioned above
        IConnection connection = factory.CreateConnection();
        //Here we create channel with session and model  
        _channel = connection.CreateModel();
    }

    public void Publish<T>(T @event) where T : IntegrationEvent
    {
        _channel.ExchangeDeclare(typeof(T).Name, ExchangeType.Fanout);
        //_channel.QueueDeclare(typeof(T).Name, exclusive: false);
        //Serialize the message
        string json = JsonConvert.SerializeObject(@event);
        byte[] body = Encoding.UTF8.GetBytes(json);
        //put the data on to the product queue
        _channel.BasicPublish(exchange: typeof(T).Name, routingKey: "", body: body);
    }

    public void Subscribe<T>(IIntegrationEventHandler<T> handler) where T : IntegrationEvent
    {
        _channel.ExchangeDeclare(exchange: typeof(T).Name, type: ExchangeType.Fanout);
        var queueName = _channel.QueueDeclare(queue: "ManagementService_" + typeof(T).Name).QueueName;
        _channel.QueueBind(queue: queueName,
                              exchange: typeof(T).Name,
                              routingKey: "");
        //Set Event object which listen message from chanel which is sent by producer
        EventingBasicConsumer consumer = new(_channel);
        consumer.Received += (model, eventArgs) =>
        {
            byte[] body = eventArgs.Body.ToArray();
            T? @event = JsonConvert.DeserializeObject<T>(Encoding.UTF8.GetString(body));
            _ = handler.Handle(@event);
        };
        _consumers.Add(queueName, consumer);
    }

    public void Consume()
    {
        foreach (var consumer in _consumers)
        {
            _channel.BasicConsume(queue: consumer.Key, autoAck: true, consumer: consumer.Value);
        }
    }
}
