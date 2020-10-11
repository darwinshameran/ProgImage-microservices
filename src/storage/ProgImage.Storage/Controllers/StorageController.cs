using System;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ProgImage.Storage.Controllers.DTO;
using ProgImage.Storage.Domain.Models;
using ProgImage.Storage.Domain.Services;
using ProgImage.Storage.Domain.Services.RO;

namespace ProgImage.Storage.Controllers
{
    [ApiController]
    [Route("/api/v1/progimage/[controller]")]
    public class StorageController : Controller
    {
        private readonly IImageService _imageService;

        private readonly IMapper _mapper;

        public StorageController(IImageService imageService, IMapper mapper)
        {
            _imageService = imageService;
            _mapper = mapper;
        }

        // POST /api/v1/progimage/storage
        [HttpPost]
        public async Task<IActionResult> SaveImageAsync(IFormFile image)
        {
            if (image == null)
            {
                return BadRequest("Error: unable to receive image.");
            }

            ImageUploadResponse response = await _imageService.AddImageAsync(image);

            if (!response.Success) return BadRequest(response.Message);

            ImageDTO imageUploadResponse = _mapper.Map<Image, ImageDTO>(response.Image);

            return Ok(imageUploadResponse);
        }

        // GET /api/v1/progimage/storage/{imageId}.{extension}
        [HttpGet]
        [Route("{imageId}.{extension?}")]
        public async Task<IActionResult> GetImageAsync(string imageId, string extension)
        {
            
            ImageFindResponse imageFound = await _imageService.FindByImageIdAsync(new Guid(imageId));

            if (!imageFound.Success)
            {
                return BadRequest(imageFound.Message);
            }

            ImageResponse imageResponse = await _imageService.GetImageBytesAsync(imageFound.Image.ImageFilePath, extension);

            if (!imageResponse.Success)
            {
                return BadRequest(imageResponse.Message);
            }

            extension ??= imageFound.Image.ImageFilePath.Split(".")[1];
            
            return File(imageResponse.Image, $"image/{extension}");
        }
    }
}