using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using ProgImage.Transformation.Controllers.DTO.Response;
using ProgImage.Transformation.Domain.Services;

namespace ProgImage.Transformation.Controllers
{
    [ApiController]
    [Route("/api/v1/progimage/transformation/[controller]")]
    public class StatusController : Controller
    {
        private readonly IStatusService _statusService;
        
        public StatusController(IStatusService statusService)
        {
            _statusService = statusService;
        }
        
        // GET /api/v1/progimage/transformation/status/{statusId}
        [HttpGet]
        [Route("{statusId}")]
        public async Task<IActionResult> TransformImageByImageId(Guid statusId)
        {
            TransformationStatusResponse status = await _statusService.FindByStatusIdAsync(statusId);

            return Ok(status);
        }
    }
}