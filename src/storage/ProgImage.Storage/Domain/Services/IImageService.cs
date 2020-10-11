using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using ProgImage.Storage.Domain.Services.RO;

namespace ProgImage.Storage.Domain.Services
{
    public interface IImageService
    {
        Task<ImageUploadResponse> AddImageAsync(IFormFile imageUploadFile);
        Task<ImageFindResponse> FindByImageIdAsync(Guid imageId);

        Task<ImageResponse> GetImageBytesAsync(string filePath, string extension);
    }
}