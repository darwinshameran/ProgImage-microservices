using System.Threading.Tasks;

namespace ProgImage.Compress.RabbitMQ.Services
{
    public interface IProducer
    {
        public Task Push(object message, string bindingKey);
    }
}