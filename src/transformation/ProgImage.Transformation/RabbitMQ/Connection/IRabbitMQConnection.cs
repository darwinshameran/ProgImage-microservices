using RabbitMQ.Client;

namespace ProgImage.Transformation.RabbitMQ.Connection
{
    public interface IRabbitMqConnection
    {
        IConnection CreateConnection();
    }
}