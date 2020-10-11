using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using ProgImage.Transformation.Controllers.DTO.Response;

namespace ProgImage.Transformation.Domain.Services
{
    public interface ICompressService
    {
        Task<TransformationStatusResponse> TransformImage(Guid imageId, int quality);
        Task<TransformationStatusResponse> TransformImage(IFormFile image, int quality);
        Task<TransformationStatusResponse> TransformImage(string url, int quality);

    }
}