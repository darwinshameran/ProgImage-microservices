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
    public class RotateController : Controller
    {
        private readonly IRotateService _rotateService;
        
        public RotateController(IRotateService rotateService)
        {
            _rotateService = rotateService;
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
            
            TransformationStatusResponse response = await _rotateService.TransformImage(imageId, (int)degrees);
            
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

            TransformationStatusResponse response = await _rotateService.TransformImage(image, (int)degrees);

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
            
            TransformationStatusResponse response = await _rotateService.TransformImage(url, (int)degrees);

            return Accepted(response);
        }
    }
}