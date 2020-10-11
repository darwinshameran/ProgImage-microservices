using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using ProgImage.Transformation.Controllers.DTO.Response;

namespace ProgImage.Transformation.Domain.Services
{
    public interface IMaskService
    {
        Task<TransformationStatusResponse> TransformImage(Guid imageId);
        Task<TransformationStatusResponse> TransformImage(IFormFile image);
        Task<TransformationStatusResponse> TransformImage(string url);

    }
}