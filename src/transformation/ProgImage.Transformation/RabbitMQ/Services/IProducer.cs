using System.Threading.Tasks;

namespace ProgImage.Transformation.RabbitMQ.Services
{
    public interface IProducer
    {
        Task Push(object message, string routingKey);
    }
}