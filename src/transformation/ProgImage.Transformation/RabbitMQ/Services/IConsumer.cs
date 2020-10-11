namespace ProgImage.Transformation.RabbitMQ.Services
{
    public interface IConsumer
    {
        public void Receive();
        public void Stop();
    }
}