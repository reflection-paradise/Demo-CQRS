using Application.Command.EventModel;
using Microsoft.Extensions.Hosting;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System.Text;
using System.Text.Json;
using Infrastructure.Consumers;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

public class RabbitMQListener : BackgroundService
{
    private readonly IModel _channel;
    private readonly IServiceProvider _serviceProvider;

    public RabbitMQListener(IServiceProvider serviceProvider, RabbitMQConnectionManager connectionManager)
    {
        _serviceProvider = serviceProvider;
        _channel = connectionManager.Channel;

        _channel.ExchangeDeclare("product_exchange", ExchangeType.Direct, durable: true);
        _channel.QueueDeclare("product_created_queue", durable: true, exclusive: false, autoDelete: false, arguments: null);
        _channel.QueueBind("product_created_queue", "product_exchange", "product.created");
    }

    protected override Task ExecuteAsync(CancellationToken stoppingToken)
    {
        var consumer = new EventingBasicConsumer(_channel);

        consumer.Received += (model, ea) =>
        {
            var json = Encoding.UTF8.GetString(ea.Body.ToArray());
            var productEvent = JsonSerializer.Deserialize<ProductCreatedEvent>(json);
            Console.WriteLine($"[Consumer] Received message from RabbitMQ: {json}");

            using var scope = _serviceProvider.CreateScope();
            var handler = scope.ServiceProvider.GetRequiredService<ProductCreatedConsumer>();
            handler.Consume(productEvent);
        };

        _channel.BasicConsume("product_created_queue", autoAck: true, consumer: consumer);
        return Task.CompletedTask;
    }

}
