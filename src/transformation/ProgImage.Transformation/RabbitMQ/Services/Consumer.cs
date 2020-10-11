using System;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.Extensions.DependencyInjection;
using ProgImage.Transformation.Domain.Events;
using ProgImage.Transformation.Domain.Models;
using ProgImage.Transformation.Domain.Services;
using ProgImage.Transformation.Helpers;
using ProgImage.Transformation.RabbitMQ.Connection;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using Serilog;

namespace ProgImage.Transformation.RabbitMQ.Services
{
    public class Consumer : IConsumer
    {
        private readonly IConnection _rabbitMqConnection;
        private readonly IServiceProvider _serviceProvider;

        private readonly IMapper _mapper;
        
        private readonly IModel _channel;
        private readonly AsyncEventingBasicConsumer _consumer;
        private string _consumerTag;

        public Consumer(IRabbitMqConnection rabbitMq, IServiceProvider serviceProvider, IMapper mapper)
        {
            _rabbitMqConnection = rabbitMq.CreateConnection();
            _serviceProvider = serviceProvider;
            _mapper = mapper;
            _channel = _rabbitMqConnection.CreateModel();
            _consumer = new AsyncEventingBasicConsumer(_channel);

            _channel.BasicQos(0, 1, false);
            _channel.ExchangeDeclare(EnvVariables.RabbitMqExchangeName, EnvVariables.RabbitMqExchangeType, true);
            _channel.QueueDeclare(EnvVariables.RabbitMqQueueName, true, autoDelete: false, exclusive: false);
            _channel.QueueBind(EnvVariables.RabbitMqQueueName, EnvVariables.RabbitMqExchangeName,  EnvVariables.RabbitMqConsumerBindingKey);
        }

        public void Receive()
        {
            _consumer.Received += OnConsumerOnReceived;

            _consumerTag = _channel.BasicConsume(EnvVariables.RabbitMqQueueName,
                false,
                _consumer);
        }

        public void Stop()
        {
            // Gracefully shutdown.
            _channel.BasicCancel(_consumerTag);
            _channel.Close(200, "Quitting.");
            _rabbitMqConnection.Close();
        }

        private async Task OnConsumerOnReceived(object sender, BasicDeliverEventArgs ea)
        {
            TransformationStatusEvent @event = ea.Body.ToArray().ToObject<TransformationStatusEvent>();
            Log.Information($"[Transformation] received {@event.ToString<TransformationStatusEvent>()}");

            TransformationStatus status = _mapper.Map<TransformationStatus>(@event);
            IServiceScope scope = _serviceProvider.CreateScope(); 
            IStatusService handler = scope.ServiceProvider.GetRequiredService<IStatusService>();
            
            await handler.UpdateStatusAsync(status);
                 
            Log.Information($"[Transformation] received {@event.ToString<TransformationStatusEvent>()}");

            _channel.BasicAck(ea.DeliveryTag, false);
        }
    }
}