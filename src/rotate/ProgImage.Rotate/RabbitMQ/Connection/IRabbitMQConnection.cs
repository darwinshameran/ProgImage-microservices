using RabbitMQ.Client;

namespace ProgImage.Rotate.RabbitMQ.Connection
{
    public interface IRabbitMqConnection
    {
        IConnection CreateConnection();
    }
}