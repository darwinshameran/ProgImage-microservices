using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using ProgImage.Transformation.Controllers.DTO.Response;

namespace ProgImage.Transformation.Domain.Services
{
    public interface IRotateService
    {
        Task<TransformationStatusResponse> TransformImage(Guid imageId, int degrees);
        Task<TransformationStatusResponse> TransformImage(IFormFile image, int degrees);
        Task<TransformationStatusResponse> TransformImage(string url, int degrees);

    }
}