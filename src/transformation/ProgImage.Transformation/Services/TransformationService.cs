using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using ProgImage.Transformation.Controllers.DTO.Response;
using ProgImage.Transformation.Domain.Events;
using ProgImage.Transformation.Domain.Models;
using ProgImage.Transformation.Domain.Services;
using ProgImage.Transformation.Helpers;
using ProgImage.Transformation.Models.DTO;
using ProgImage.Transformation.RabbitMQ.Services;

namespace ProgImage.Transformation.Services
{
    public class ImageTransform<T> : IImageTransform<T> where T : class
    {
        private readonly IStatusService _statusService;
        private readonly IProducer _producer;

        public ImageTransform(IStatusService statusService, IProducer producer)
        {
            _statusService = statusService;
            _producer = producer;
        }

        public async Task<TransformationStatusResponse> Transform<T>(T @event, string routingKey)
            where T : BaseEvent
        {
            _producer.Push(@event, routingKey);

            TransformationStatusResponse transformationStatusResponse = await CreateStatus(@event.StatusId, "Queued");

            return transformationStatusResponse;
        }

        public async Task<TransformationStatusResponse> Transform<T>(T @event, string routingKey, IFormFile file)
            where T : BaseEvent
        {
            string url = "http://progimage-storage:8080/api/v1/progimage/storage/";
            Image uploadedImage = await HttpHelper.PostImageAsync(file, url);
            @event.Url = $"{url}{uploadedImage.ImageId}";

            return await Transform(@event, routingKey);
        }

        private async Task<TransformationStatusResponse> CreateStatus(Guid statusId, string status)
        {
            TransformationStatus transformationStatus = new TransformationStatus
            {
                StatusId = statusId,
                ImageId = null,
                Status = status
            };

            TransformationStatusResponse transformationStatusResponse =
                await _statusService.AddStatusAsync(transformationStatus);

            return transformationStatusResponse;
        }
    }
}