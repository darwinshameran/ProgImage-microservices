using System.Threading.Tasks;

namespace ProgImage.Blur.RabbitMQ.Services
{
    public interface IProducer
    {
        public Task Push(object message, string bindingKey);
    }
}