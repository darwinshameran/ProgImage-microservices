using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ProgImage.Transformation.Controllers.DTO.Response;
using ProgImage.Transformation.Domain.Events;
using ProgImage.Transformation.Domain.Services;
using ProgImage.Transformation.Helpers;

namespace ProgImage.Transformation.Controllers
{
    [ApiController]
    [Route("/api/v1/progimage/transformation")]
    public class ThumbnailController : Controller
    {
        private readonly IImageTransform<BaseEvent> _imageTransform;
        
        public ThumbnailController(IImageTransform<BaseEvent> imageTransform)
        {
            _imageTransform = imageTransform;
        }
        
        // POST /api/v1/progimage/transformation/{imageId}/thumbnail?width={width}&height={height}
        [HttpPost]
        [Route("{imageId}/[controller]")]
        public async Task<IActionResult> TransformImageByImageId(Guid imageId, [FromQuery] int? width, [FromQuery] int? height)
        {
            if (!width.HasValue || !height.HasValue)
            {
                return BadRequest("Error: `width` and/or `quality` query string not set.");
            }
            
            TransformationStatusResponse response = await _imageTransform.Transform(new TransformationThumbnailStartEvent
            {
                StatusId = Guid.NewGuid(),
                Url = $"http://progimage-storage:8080/api/v1/progimage/storage/{imageId}",
                Width = (int)width,
                Height = (int)height
            }, "progimage.transformation.thumbnail");
            
            return Accepted(response);
        }
        
        // POST /api/v1/progimage/transformation/thumbnail?width={width}&height={height}
        [HttpPost]
        [ExactQueryParamAttribute("width", "height")]
        [Route("[controller]")]
        public async Task<IActionResult> TransformImageByData(IFormFile image, [FromQuery] int? width, [FromQuery] int? height)
        {
            if (!width.HasValue || !height.HasValue)
            {
                return BadRequest("Error: `width` and/or `quality` query strings not set.");
            }
            
            TransformationStatusResponse response = await _imageTransform.Transform(new TransformationThumbnailStartEvent
            {
                StatusId = Guid.NewGuid(),
                Width = (int)width,
                Height = (int)height
            }, "progimage.transformation.thumbnail", image);
            
            return Accepted(response);
        }
        
        // POST /api/v1/progimage/transformation/thumbnail?url={url}&width={width}&height={height}
        [HttpPost]
        [ExactQueryParamAttribute("url", "width", "height")]
        [Route("[controller]")]
        public async Task<IActionResult> TransformImageByUrl([FromQuery] string url, [FromQuery] int? width, [FromQuery] int? height)
        {
            if (!width.HasValue || !height.HasValue)
            {
                return BadRequest("Error: `width` and/or `quality` query strings not set.");
            }
            
            TransformationStatusResponse response = await _imageTransform.Transform(new TransformationThumbnailStartEvent
            {
                StatusId = Guid.NewGuid(),
                Url = url,
                Width = (int)width,
                Height = (int)height
            }, "progimage.transformation.thumbnail");
            
            return Accepted(response);
        }
    }
}