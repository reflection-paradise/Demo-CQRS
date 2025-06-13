using Application.Command.EventModel;
using Microsoft.Extensions.Hosting;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System.Text;
using System.Text.Json;
using Infrastructure.Consumers;
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

        // Queue for Created
        _channel.QueueDeclare("product_created_queue", durable: true, exclusive: false, autoDelete: false);
        _channel.QueueBind("product_created_queue", "product_exchange", "product.created");

        // Queue for Updated
        _channel.QueueDeclare("product_updated_queue", durable: true, exclusive: false, autoDelete: false);
        _channel.QueueBind("product_updated_queue", "product_exchange", "product.updated");

        // Queue for Deleted
        _channel.QueueDeclare("product_deleted_queue", durable: true, exclusive: false, autoDelete: false);
        _channel.QueueBind("product_deleted_queue", "product_exchange", "product.deleted");
    }

    protected override Task ExecuteAsync(CancellationToken stoppingToken)
    {
        StartConsumer("product_created_queue", "product.created");
        StartConsumer("product_updated_queue", "product.updated");
        StartConsumer("product_deleted_queue", "product.deleted");

        return Task.CompletedTask;
    }

    private void StartConsumer(string queueName, string routingKey)
    {
        var consumer = new EventingBasicConsumer(_channel);
        consumer.Received += (model, ea) =>
        {
            var json = Encoding.UTF8.GetString(ea.Body.ToArray());
            Console.WriteLine($"[Consumer] Received message ({routingKey}): {json}");

            using var scope = _serviceProvider.CreateScope();

            if (routingKey == "product.created" || routingKey == "product.updated")
            {
                var productEvent = JsonSerializer.Deserialize<ProductCreatedEvent>(json);
                var handler = scope.ServiceProvider.GetRequiredService<ProductCreatedConsumer>();
                handler.Consume(productEvent);
            }
            else if (routingKey == "product.deleted")
            {
                var deleteEvent = JsonSerializer.Deserialize<ProductCreatedEvent>(json);
                var handler = scope.ServiceProvider.GetRequiredService<ProductDeletedConsumer>();
                handler.Consume(deleteEvent);
            }
        };

        _channel.BasicConsume(queueName, autoAck: true, consumer: consumer);
    }
}
