using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ProgImage.Resize.Events;
using ProgImage.Transformation.Controllers.DTO.Response;
using ProgImage.Transformation.Domain.Events;
using ProgImage.Transformation.Domain.Services;
using ProgImage.Transformation.Helpers;

namespace ProgImage.Transformation.Controllers
{
    [ApiController]
    [Route("/api/v1/progimage/transformation")]
    public class CompressController : Controller
    {
        private readonly IImageTransform<BaseEvent> _imageTransform;

        public CompressController(IImageTransform<BaseEvent> imageTransform)
        {
            _imageTransform = imageTransform;
        }
        
        // POST /api/v1/progimage/transformation/{imageId}/compress?quality={quality}
        [HttpPost]
        [Route("{imageId}/[controller]")]
        public async Task<IActionResult> TransformImageByImageId(Guid imageId, [FromQuery] int? quality)
        {
            if (!quality.HasValue)
            {
                return BadRequest("Error: `quality` query string not set.");
            }
            
            TransformationStatusResponse response = await _imageTransform.Transform(new TransformationCompressStartEvent
            {
                StatusId = Guid.NewGuid(),
                Url = $"http://progimage-storage:8080/api/v1/progimage/storage/{imageId}",
                Quality = (int)quality,
            }, "progimage.transformation.compress");
            
            return Accepted(response);
        }
        
        // POST /api/v1/progimage/transformation/compress?quality={quality}
        [HttpPost]
        [ExactQueryParamAttribute("quality")]
        [Route("[controller]")]
        public async Task<IActionResult> TransformImageByData(IFormFile image, [FromQuery] int? quality)
        {
            if (!quality.HasValue)
            {
                return BadRequest("Error: `quality` query string not set.");
            }
            
            TransformationStatusResponse response = await _imageTransform.Transform(new TransformationCompressStartEvent
            {
                StatusId = Guid.NewGuid(),
                Quality = (int)quality,
            }, "progimage.transformation.compress", image);
            
            return Accepted(response);
        }
        
        // POST /api/v1/progimage/transformation/compress?url={url}&quality={quality}
        [HttpPost]
        [ExactQueryParamAttribute("url", "quality")]
        [Route("[controller]")]
        public async Task<IActionResult> TransformImageByUrl([FromQuery] string url, [FromQuery] int? quality)
        {
            if (!quality.HasValue)
            {
                return BadRequest("Error: `quality` query string not set.");
            }
            
            TransformationStatusResponse response = await _imageTransform.Transform(new TransformationCompressStartEvent
            {
                StatusId = Guid.NewGuid(),
                Url = url,
                Quality = (int)quality,
            }, "progimage.transformation.compress");

            return Accepted(response);
        }
    }
}