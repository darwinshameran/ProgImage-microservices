using ProgImage.Transformation.Helpers;
using RabbitMQ.Client;

namespace ProgImage.Transformation.RabbitMQ.Connection
{

    public class RabbitMqConnection : IRabbitMqConnection
    {
        public IConnection CreateConnection()
        {
            ConnectionFactory factory = new ConnectionFactory
            {
                DispatchConsumersAsync = true,
                HostName = EnvVariables.RabbitMqHostname,
                Port = int.Parse(EnvVariables.RabbitMqPort),
                UserName = EnvVariables.RabbitMqUsername,
                Password = EnvVariables.RabbitMqPassword
            };

            IConnection connection = factory.CreateConnection();

            return connection;
        }
    }
}