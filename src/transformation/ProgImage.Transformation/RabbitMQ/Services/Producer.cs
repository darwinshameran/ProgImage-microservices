using System.Threading.Tasks;
using ProgImage.Transformation.Helpers;
using ProgImage.Transformation.RabbitMQ.Connection;
using RabbitMQ.Client;

namespace ProgImage.Transformation.RabbitMQ.Services
{
    public class Producer : IProducer
    {
        private readonly IModel _channel;

        public Producer(IRabbitMqConnection rabbitMq)
        {
            IConnection rabbitMqConnection = rabbitMq.CreateConnection();
            _channel = rabbitMqConnection.CreateModel();

            _channel.ExchangeDeclare(EnvVariables.RabbitMqExchangeName, EnvVariables.RabbitMqExchangeType, true);
        }

        public async Task Push(object message, string routingKey)
        {
            IBasicProperties properties = _channel.CreateBasicProperties();
            _channel.BasicPublish(EnvVariables.RabbitMqExchangeName, routingKey, 
                                  body: message.ToBytes(), basicProperties: properties);
        }
    }
}