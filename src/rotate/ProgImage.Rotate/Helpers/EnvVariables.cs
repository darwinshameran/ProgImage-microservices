using System;

namespace ProgImage.Rotate.Helpers
{
    public static class EnvVariables
    {
        public static string RabbitMqHostname { get; } = Environment.GetEnvironmentVariable("RABBITMQ_HOSTNAME");
        public static string RabbitMqPort { get; } = Environment.GetEnvironmentVariable("RABBITMQ_PORT");
        public static string RabbitMqUsername { get; } = Environment.GetEnvironmentVariable("RABBITMQ_USERNAME");
        public static string RabbitMqPassword { get; } = Environment.GetEnvironmentVariable("RABBITMQ_PASSWORD");
        public static string RabbitMqExchangeName { get; } = Environment.GetEnvironmentVariable("RABBITMQ_EXCHANGE_NAME");
        public static string RabbitMqQueueName { get; } = Environment.GetEnvironmentVariable("RABBITMQ_QUEUE_NAME");
        public static string RabbitMqExchangeType { get; } = Environment.GetEnvironmentVariable("RABBITMQ_EXCHANGE_TYPE");
        public static string RabbitMqConsumerBindingKey { get; } = Environment.GetEnvironmentVariable("RABBITMQ_CONSUMER_BINDING_KEY");
        public static string RabbitMqProducerBindingKey { get; } = Environment.GetEnvironmentVariable("RABBITMQ_PRODUCER_BINDING_KEY");
    }
}