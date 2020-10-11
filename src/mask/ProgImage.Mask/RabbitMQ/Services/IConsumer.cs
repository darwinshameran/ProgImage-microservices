namespace ProgImage.Mask.RabbitMQ.Services
{
    public interface IConsumer
    {
        public void Receive();
        public void Stop();
    }
}