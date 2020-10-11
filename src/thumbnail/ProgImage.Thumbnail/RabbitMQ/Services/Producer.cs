using System.Threading.Tasks;
using ProgImage.Resize.Helpers;
using ProgImage.Resize.RabbitMQ.Connection;
using RabbitMQ.Client;

namespace ProgImage.Resize.RabbitMQ.Services
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

        public async Task Push(object message, string bindingKey)
        {
            IBasicProperties properties = _channel.CreateBasicProperties();
            _channel.BasicPublish(EnvVariables.RabbitMqExchangeName, bindingKey,
                body: message.ToBytes(), basicProperties: properties);
        }
    }
}