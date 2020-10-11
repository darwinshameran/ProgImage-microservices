using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using ProgImage.Resize.Events;
using ProgImage.Transformation.Controllers.DTO.Response;
using ProgImage.Transformation.Domain.Events;
using ProgImage.Transformation.Domain.Services;
using ProgImage.Transformation.Helpers;
using ProgImage.Transformation.Models;
using ProgImage.Transformation.Models.DTO;
using ProgImage.Transformation.RabbitMQ.Services;
using TransformationStatus = ProgImage.Transformation.Domain.Models.TransformationStatus;

namespace ProgImage.Transformation.Services
{
    public class BlurService : IBlurService
    {
        private readonly IStatusService _statusService;
        private readonly IProducer _producer;
        
        public BlurService(IStatusService statusService, IProducer producer)
        {
            _statusService = statusService;
            _producer = producer;
        }

        public async Task<TransformationStatusResponse> TransformImage(Guid imageId, double radius, double sigma)
        {
            Guid statusId = Guid.NewGuid();
            
            TransformationBlurStartEvent @event = new TransformationBlurStartEvent()
            {
                StatusId = statusId,
                Url = $"http://progimage-storage:8080/api/v1/progimage/storage/{imageId}",
                Radius = radius,
                Sigma = sigma
            };
            
            _producer.Push(@event, "progimage.transformation.blur");
            
            TransformationStatusResponse transformationStatusResponse = await CreateStatus(statusId, "Queued");

            return transformationStatusResponse;
        }
        
        public async Task<TransformationStatusResponse> TransformImage(IFormFile image, double radius, double sigma)
        {
            Guid statusId = Guid.NewGuid();
            Image uploadedImage = await HttpHelper.PostImageAsync(image, "http://progimage-storage:8080/api/v1/progimage/storage");
            
            TransformationBlurStartEvent @event = new TransformationBlurStartEvent
            {
                StatusId = statusId,
                Url = $"http://progimage-storage:8080/api/v1/progimage/storage/{uploadedImage.ImageId}",
                Radius = radius,
                Sigma = sigma
            };
            
            _producer.Push(@event, "progimage.transformation.blur");
            
            TransformationStatusResponse transformationStatusResponse = await CreateStatus(statusId, "Queued");

            return transformationStatusResponse;            
        }

        public async Task<TransformationStatusResponse> TransformImage(string url, double radius, double sigma)
        {
            Guid statusId = Guid.NewGuid();
            
            TransformationBlurStartEvent @event = new TransformationBlurStartEvent
            {
                StatusId = statusId,
                Url = url,
                Radius = radius,
                Sigma = sigma
            };
            
            _producer.Push(@event, "progimage.transformation.blur");
            
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