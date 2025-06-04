using Application.IMessageBus;
using RabbitMQ.Client;
using System.Text;

namespace Infrastructure.MessageBus
{
    public class RabbitMQPublisher : IRabbitMQPublisher
    {
        private readonly IModel _channel;

        public RabbitMQPublisher(RabbitMQConnectionManager connectionManager)
        {
            _channel = connectionManager.Channel;
            _channel.ExchangeDeclare("product_exchange", ExchangeType.Direct, durable: true);
        }

        public void Publish(string exchange, string routingKey, byte[] body)
        {
            _channel.BasicPublish(exchange, routingKey, null, body);
            Console.WriteLine($"[Publisher] Published message to exchange: {exchange}, routingKey: {routingKey}, body: {Encoding.UTF8.GetString(body)}");

        }
    }

}