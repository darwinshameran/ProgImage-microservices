using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ProgImage.Resize.Events;
using ProgImage.Transformation.Controllers.DTO.Response;
using ProgImage.Transformation.Domain.Events;
using ProgImage.Transformation.Domain.Services;
using ProgImage.Transformation.Helpers;
using ProgImage.Transformation.Services;

namespace ProgImage.Transformation.Controllers
{
    [ApiController]
    [Route("/api/v1/progimage/transformation")]
    public class BlurController : Controller
    {
        private readonly IImageTransform<BaseEvent> _imageTransform;

        public BlurController(IImageTransform<BaseEvent> imageTransform)
        {
            _imageTransform = imageTransform;
        }
        
        // POST /api/v1/progimage/transformation/{imageId}/blur?radius={radius}&sigma={sigma}
        [HttpPost]
        [Route("{imageId}/[controller]")]
        public async Task<IActionResult> TransformImageByImageId(Guid imageId, [FromQuery] double? radius, [FromQuery] double? sigma)
        {
            if (!radius.HasValue && !sigma.HasValue)
            {
                return BadRequest("Error: `Radius` and `sigma` query strings not set.");
            }
            
            TransformationStatusResponse response = await _imageTransform.Transform(new TransformationBlurStartEvent
            {
                StatusId = Guid.NewGuid(),
                Url = $"http://progimage-storage:8080/api/v1/progimage/storage/{imageId}",
                Radius = (double)radius,
                Sigma = (double)sigma
            }, "progimage.transformation.blur");
            
            return Accepted(response);
        }
        
        // POST /api/v1/progimage/transformation/blur?radius={radius}&sigma={sigma}
        [HttpPost]
        [ExactQueryParamAttribute("radius", "sigma")]
        [Route("[controller]")]
        public async Task<IActionResult> TransformImageByData(IFormFile image, [FromQuery] double? radius, [FromQuery] double? sigma)
        {
            if (!radius.HasValue && !sigma.HasValue)
            {
                return BadRequest("Error: `Radius` and `sigma` query strings not set.");
            }
            
            TransformationStatusResponse response = await _imageTransform.Transform(new TransformationBlurStartEvent
            {
                StatusId = Guid.NewGuid(),
                Radius = (double)radius,
                Sigma = (double)sigma
            }, "progimage.transformation.blur", image);
            
            return Accepted(response);
        }
        
        // POST /api/v1/progimage/transformation/blur?url={url}&radius={radius}&sigma={sigma}
        [HttpPost]
        [ExactQueryParamAttribute("url", "radius", "sigma")]
        [Route("[controller]")]
        public async Task<IActionResult> TransformImageByUrl([FromQuery] string url, [FromQuery] double? radius, [FromQuery] double? sigma)
        {
            if (!radius.HasValue && !sigma.HasValue)
            {
                return BadRequest("Error: `Radius` and `sigma` query strings not set.");
            }
            
            if (string.IsNullOrEmpty(url))
            {
                return BadRequest("Error: `url` query string not set.");
            }
            
            TransformationStatusResponse response = await _imageTransform.Transform(new TransformationBlurStartEvent
            {
                StatusId = Guid.NewGuid(),
                Url = url,
                Radius = (double)radius,
                Sigma = (double)sigma
            }, "progimage.transformation.blur");
            
            return Accepted(response);
        }
    }
}