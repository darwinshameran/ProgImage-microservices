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
    public class MaskController : Controller
    {
        private readonly IMaskService _maskService;
        
        public MaskController(IMaskService maskService)
        {
            _maskService = maskService;
        }
        
        // POST /api/v1/progimage/transformation/{imageId}/mask
        [HttpPost]
        [Route("{imageId}/[controller]")]
        public async Task<IActionResult> TransformImageByImageId(Guid imageId)
        {
            TransformationStatusResponse response = await _maskService.TransformImage(imageId);
            
            return Accepted(response);
        }
        
        // POST /api/v1/progimage/transformation/mask
        [HttpPost]
        [Route("[controller]")]
        public async Task<IActionResult> TransformImageByData(IFormFile image)
        {
            TransformationStatusResponse response = await _maskService.TransformImage(image);

            return Accepted(response);
        }
        
        // POST /api/v1/progimage/transformation/mask?url={url}
        [HttpPost]
        [ExactQueryParamAttribute("url")]
        [Route("[controller]")]
        public async Task<IActionResult> TransformImageByUrl([FromQuery] string url)
        { 
            TransformationStatusResponse response = await _maskService.TransformImage(url);

            return Accepted(response);
        }
    }
}