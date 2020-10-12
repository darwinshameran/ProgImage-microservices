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
    public class MaskController : Controller
    {
        private readonly IImageTransform<BaseEvent> _imageTransform;
        
        public MaskController(IImageTransform<BaseEvent> imageTransform)
        {
            _imageTransform = imageTransform;
        }
        
        // POST /api/v1/progimage/transformation/{imageId}/mask
        [HttpPost]
        [Route("{imageId}/[controller]")]
        public async Task<IActionResult> TransformImageByImageId(Guid imageId)
        {
            TransformationStatusResponse response = await _imageTransform.Transform(new TransformationMaskStartEvent
            {
                StatusId = Guid.NewGuid(),
                Url = $"http://progimage-storage:8080/api/v1/progimage/storage/{imageId}",
            }, "progimage.transformation.mask");
            
            return Accepted(response);
        }
        
        // POST /api/v1/progimage/transformation/mask
        [HttpPost]
        [Route("[controller]")]
        public async Task<IActionResult> TransformImageByData(IFormFile image)
        {
            TransformationStatusResponse response = await _imageTransform.Transform(new TransformationMaskStartEvent
            {
                StatusId = Guid.NewGuid()
            }, "progimage.transformation.mask", image);

            return Accepted(response);
        }
        
        // POST /api/v1/progimage/transformation/mask?url={url}
        [HttpPost]
        [ExactQueryParamAttribute("url")]
        [Route("[controller]")]
        public async Task<IActionResult> TransformImageByUrl([FromQuery] string url)
        { 
            TransformationStatusResponse response = await _imageTransform.Transform(new TransformationMaskStartEvent
            {
                StatusId = Guid.NewGuid(),
                Url = url,
            }, "progimage.transformation.mask");

            return Accepted(response);
        }
    }
}