using System.Threading.Tasks;

namespace ProgImage.Rotate.RabbitMQ.Services
{
    public interface IProducer
    { 
        Task Push(object message, string bindingKey);
    }
}