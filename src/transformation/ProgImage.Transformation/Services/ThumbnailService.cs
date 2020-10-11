using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using ProgImage.Transformation.Controllers.DTO.Response;
using ProgImage.Transformation.Domain.Events;
using ProgImage.Transformation.Domain.Services;
using ProgImage.Transformation.Helpers;
using ProgImage.Transformation.Models.DTO;
using ProgImage.Transformation.RabbitMQ.Services;
using TransformationStatus = ProgImage.Transformation.Domain.Models.TransformationStatus;

namespace ProgImage.Transformation.Services
{
    public class ThumbnailService : IThumbnailService
    {
        private readonly IStatusService _statusService;
        private readonly IProducer _producer;
        
        public ThumbnailService(IStatusService statusService, IProducer producer)
        {
            _statusService = statusService;
            _producer = producer;
        }

        public async Task<TransformationStatusResponse> TransformImage(Guid imageId, int width, int height)
        {
            Guid statusId = Guid.NewGuid();
            TransformationThumbnailStartEvent @event = new TransformationThumbnailStartEvent
            {
                StatusId = statusId,
                Url = $"http://progimage-storage:8080/api/v1/progimage/storage/{imageId}",
                Width = width,
                Height = height
            };
            
            _producer.Push(@event, "progimage.transformation.thumbnail");
            
            TransformationStatusResponse transformationStatusResponse = await CreateStatus(statusId, "Queued");

            return transformationStatusResponse;
        }
        
        public async Task<TransformationStatusResponse> TransformImage(IFormFile image, int width, int height)
        {
            Guid statusId = Guid.NewGuid();
            Image uploadedImage = await HttpHelper.PostImageAsync(image, "http://progimage-storage:8080/api/v1/progimage/storage");
            
            TransformationThumbnailStartEvent @event = new TransformationThumbnailStartEvent
            {
                StatusId = statusId,
                Url = $"http://progimage-storage:8080/api/v1/progimage/storage/{uploadedImage.ImageId}",
                Width = width,
                Height = height
            };
            
            _producer.Push(@event, "progimage.transformation.thumbnail");
            
            TransformationStatusResponse transformationStatusResponse = await CreateStatus(statusId, "Queued");

            return transformationStatusResponse;            
        }

        public async Task<TransformationStatusResponse> TransformImage(string url, int width, int height)
        {
            Guid statusId = Guid.NewGuid();
            
            TransformationThumbnailStartEvent @event = new TransformationThumbnailStartEvent
            {
                StatusId = statusId,
                Url = url,
                Width = width,
                Height = height
            };
            
            _producer.Push(@event, "progimage.transformation.thumbnail");
            
            TransformationStatusResponse transformationStatusResponse = await CreateStatus(statusId, "Queued");

            return transformationStatusResponse;
        }
        
        private async Task<TransformationStatusResponse> CreateStatus(Guid statusId, string status)
        {
            TransformationStatus transformationStatus = new TransformationStatus
            {
                StatusId = statusId,
                ImageId = null,
                Status = status
            };

            TransformationStatusResponse transformationStatusResponse = await _statusService.AddStatusAsync(transformationStatus);
            
            return transformationStatusResponse;
        }
    }
}