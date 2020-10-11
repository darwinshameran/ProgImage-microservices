using System.Threading.Tasks;

namespace ProgImage.Resize.RabbitMQ.Services
{
    public interface IProducer
    {
        public Task Push(object message, string bindingKey);
    }
}