namespace ProgImage.Compress.RabbitMQ.Services
{
    public interface IConsumer
    {
        public void Receive();
        public void Stop();
    }
}