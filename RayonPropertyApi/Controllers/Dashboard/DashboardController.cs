using Business.Abstract;
using Business.Attributes;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace RayonPropertyApi.Controllers.Dashboard
{
    [Route("api/[controller]")]
    [ApiController]
    [RayonPropertyAuthorize]
    [ApiExplorerSettings(IgnoreApi = true)]
    public class DashboardController : ControllerBase
    {
        private readonly IDashboardService _dashboardService;

        public DashboardController(IDashboardService dashboardService)
        {
            _dashboardService = dashboardService;
        }

        [HttpGet]
        public IActionResult Get()
        {
            return Ok(_dashboardService.GetDashboardInfos().Data);
        }
    }
}
