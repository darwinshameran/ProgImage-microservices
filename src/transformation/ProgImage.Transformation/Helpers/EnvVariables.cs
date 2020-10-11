using System;

namespace ProgImage.Transformation.Helpers
{
    public static class EnvVariables
    {
        public static string DatabaseHostname { get; } = Environment.GetEnvironmentVariable("DATABASE_HOSTNAME");
        public static string DatabaseName { get; } = Environment.GetEnvironmentVariable("DATABASE_NAME");
        public static string DatabaseUsername { get; } = Environment.GetEnvironmentVariable("DATABASE_USERNAME");
        public static string DatabasePassword { get; } = Environment.GetEnvironmentVariable("DATABASE_PASSWORD");
        public static string DatabasePort { get; } = Environment.GetEnvironmentVariable("DATABASE_PORT");

        public static string WebserverPort { get; } = Environment.GetEnvironmentVariable("WEBSERVER_PORT");
        public static string RabbitMqHostname { get; } = Environment.GetEnvironmentVariable("RABBITMQ_HOSTNAME");
        public static string RabbitMqPort { get; } = Environment.GetEnvironmentVariable("RABBITMQ_PORT");
        public static string RabbitMqUsername { get; } = Environment.GetEnvironmentVariable("RABBITMQ_USERNAME");
        public static string RabbitMqPassword { get; } = Environment.GetEnvironmentVariable("RABBITMQ_PASSWORD");
        public static string RabbitMqExchangeName { get; } = Environment.GetEnvironmentVariable("RABBITMQ_EXCHANGE_NAME");
        public static string RabbitMqQueueName { get; } = Environment.GetEnvironmentVariable("RABBITMQ_QUEUE_NAME");
        
        public static string RabbitMqConsumerBindingKey { get; } = Environment.GetEnvironmentVariable("RABBITMQ_CONSUMER_BINDING_KEY");
        public static string RabbitMqProducerBindingKey { get; } = Environment.GetEnvironmentVariable("RABBITMQ_PRODUCER_BINDING_KEY");
        public static string RabbitMqExchangeType { get; } = Environment.GetEnvironmentVariable("RABBITMQ_EXCHANGE_TYPE");
    }
}