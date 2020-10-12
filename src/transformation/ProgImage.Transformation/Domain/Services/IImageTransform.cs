using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using ProgImage.Transformation.Controllers.DTO.Response;
using ProgImage.Transformation.Domain.Events;

namespace ProgImage.Transformation.Domain.Services
{
    public interface IImageTransform<T> where T : class
    {
        Task<TransformationStatusResponse> Transform<T>(T @event, string routingKey) where T : BaseEvent;

        Task<TransformationStatusResponse> Transform<T>(T @event, string routingKey, IFormFile file)
            where T : BaseEvent;
    }
}