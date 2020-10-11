using RabbitMQ.Client;

namespace ProgImage.Mask.RabbitMQ.Connection
{
    public interface IRabbitMqConnection
    {
        IConnection CreateConnection();
    }
}