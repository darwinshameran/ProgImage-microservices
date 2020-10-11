namespace ProgImage.Resize.RabbitMQ.Services
{
    public interface IConsumer
    {
        public void Receive();
        public void Stop();
    }
}