using RabbitMQ.Client;

namespace ProgImage.Resize.RabbitMQ.Connection
{
    public interface IRabbitMqConnection
    {
        IConnection CreateConnection();
    }
}