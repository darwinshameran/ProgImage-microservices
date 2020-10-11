using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using ProgImage.Transformation.Controllers.DTO.Response;

namespace ProgImage.Transformation.Domain.Services
{
    public interface IThumbnailService
    {
        Task<TransformationStatusResponse> TransformImage(Guid imageId, int width, int height);
        Task<TransformationStatusResponse> TransformImage(IFormFile image, int width, int height);
        Task<TransformationStatusResponse> TransformImage(string url,  int width, int height);

    }
}