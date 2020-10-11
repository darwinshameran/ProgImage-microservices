using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using ProgImage.Transformation.Controllers.DTO.Response;

namespace ProgImage.Transformation.Domain.Services
{
    public interface IBlurService
    {
        Task<TransformationStatusResponse> TransformImage(Guid imageId, double radius, double sigma);
        Task<TransformationStatusResponse> TransformImage(IFormFile image, double radius, double sigma);
        Task<TransformationStatusResponse> TransformImage(string url, double radius, double sigma);

    }
}