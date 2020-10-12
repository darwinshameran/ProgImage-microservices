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
    public class RotateController : Controller
    {
        private readonly IImageTransform<BaseEvent> _imageTransform;
        
        public RotateController(IImageTransform<BaseEvent> imageTransform)
        {
            _imageTransform = imageTransform;
        }
        
        // POST /api/v1/progimage/transformation/{imageId}/rotate?degrees={degrees}
        [HttpPost]
        [Route("{imageId}/[controller]")]
        public async Task<IActionResult> TransformImageByImageId(Guid imageId, [FromQuery] int? degrees)
        {
            if (!degrees.HasValue)
            {
                return BadRequest("Error: `degrees` query string not set.");
            }
            
            TransformationStatusResponse response = await _imageTransform.Transform(new TransformationRotateStartEvent
            {
                StatusId = Guid.NewGuid(),
                Url = $"http://progimage-storage:8080/api/v1/progimage/storage/{imageId}",
                Degrees = (int)degrees
            }, "progimage.transformation.rotate");
            
            return Accepted(response);
        }
        
        // POST /api/v1/progimage/transformation/rotate?degrees={degrees}
        [HttpPost]
        [ExactQueryParamAttribute("degrees")]
        [Route("[controller]")]
        public async Task<IActionResult> TransformImageByData(IFormFile image, [FromQuery] int? degrees)
        {
            if (!degrees.HasValue)
            {
                return BadRequest("Error: `degrees` query string not set.");
            }

            TransformationStatusResponse response = await _imageTransform.Transform(new TransformationRotateStartEvent
            {
                StatusId = Guid.NewGuid(),
                Degrees = (int)degrees
            }, "progimage.transformation.rotate", image);
            
            return Accepted(response);
        }
        
        // POST /api/v1/progimage/transformation/rotate?url={url}&degrees={degrees}
        [HttpPost]
        [ExactQueryParamAttribute("url", "degrees")]
        [Route("[controller]")]
        public async Task<IActionResult> TransformImageByUrl([FromQuery] string url, [FromQuery] int? degrees)
        {
            if (!degrees.HasValue)
            {
                return BadRequest("Error: `degrees` query string not set.");
            }
            
            TransformationStatusResponse response = await _imageTransform.Transform(new TransformationRotateStartEvent
            {
                StatusId = Guid.NewGuid(),
                Url = url,
                Degrees = (int)degrees
            }, "progimage.transformation.rotate");
            

            return Accepted(response);
        }
    }
}