using RabbitMQ.Client;

namespace ProgImage.Compress.RabbitMQ.Connection
{
    public interface IRabbitMqConnection
    {
        IConnection CreateConnection();
    }
}