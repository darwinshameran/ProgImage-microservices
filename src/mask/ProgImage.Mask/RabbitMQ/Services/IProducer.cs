using System.Threading.Tasks;

namespace ProgImage.Mask.RabbitMQ.Services
{
    public interface IProducer
    { 
        Task Push(object message, string bindingKey);
    }
}