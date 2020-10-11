using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ProgImage.Transformation.Controllers.DTO.Response;
using ProgImage.Transformation.Domain.Services;
using ProgImage.Transformation.Helpers;

namespace ProgImage.Transformation.Controllers
{
    [ApiController]
    [Route("/api/v1/progimage/transformation")]
    public class BlurController : Controller
    {
        private readonly IBlurService _blurService;
        
        public BlurController(IBlurService blurService)
        {
            _blurService = blurService;
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
            
            TransformationStatusResponse response = await _blurService.TransformImage(imageId, (double)radius, (double)sigma);
            
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
            
            TransformationStatusResponse response = await _blurService.TransformImage(image, (double)radius, (double)sigma);

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
            
            TransformationStatusResponse response = await _blurService.TransformImage(url, (double)radius, (double)sigma);

            return Accepted(response);
        }
    }
}