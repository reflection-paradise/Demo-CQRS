using Microsoft.Extensions.Configuration;
using RabbitMQ.Client;

public class RabbitMQConnectionManager : IDisposable
{
    public IConnection Connection { get; }
    public IModel Channel { get; }

    public RabbitMQConnectionManager(IConfiguration config)
    {
        var factory = new ConnectionFactory
        {
            HostName = config["RabbitMQ:HostName"],
            Port = 5672
        };

        Connection = factory.CreateConnection();
        Channel = Connection.CreateModel();
    }

    public void Dispose()
    {
        Channel?.Close();
        Connection?.Close();
    }
}
