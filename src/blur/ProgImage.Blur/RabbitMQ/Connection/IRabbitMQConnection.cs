using RabbitMQ.Client;

namespace ProgImage.Blur.RabbitMQ.Connection
{
    public interface IRabbitMqConnection
    {
        IConnection CreateConnection();
    }
}