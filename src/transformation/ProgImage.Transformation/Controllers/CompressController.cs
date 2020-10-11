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
    public class CompressController : Controller
    {
        private readonly ICompressService _compressService;
        
        public CompressController(ICompressService compressService)
        {
            _compressService = compressService;
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
            
            TransformationStatusResponse response = await _compressService.TransformImage(imageId, (int)quality);
            
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
            
            TransformationStatusResponse response = await _compressService.TransformImage(image, (int)quality);

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
            
            TransformationStatusResponse response = await _compressService.TransformImage(url, (int)quality);

            return Accepted(response);
        }
    }
}