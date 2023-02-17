using Business.Abstract;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace RayonPropertyApi.Controllers.WebSite
{
    [Route("api/[controller]")]
    [ApiController]
    [AllowAnonymous]
    public class WebSiteInfoController : ControllerBase
    {
        private readonly ISitePropertyService _sitePropertyService;

        public WebSiteInfoController(ISitePropertyService sitePropertyService)
        {
            _sitePropertyService = sitePropertyService;
        }

        [HttpGet]
        public IActionResult Get()
        {
            var result = _sitePropertyService.GetSiteProperty();
            if (result.Success)
            {
                return Ok(result.Data);
            }
            return BadRequest(result.Message);
        }
    }
}
