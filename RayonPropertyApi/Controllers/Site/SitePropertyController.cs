using Business.Abstract;
using Business.Attributes;
using Entities.Dtos;
using Microsoft.AspNetCore.Mvc;

namespace RayonPropertyApi.Controllers.Site
{
    [RayonPropertyAuthorize]
    [Route("api/[controller]")]
    [ApiController]
    [ApiExplorerSettings(IgnoreApi = true)]
    public class SitePropertyController : ControllerBase
    {
        private readonly ISitePropertyService _sitePropertyService;

        public SitePropertyController(ISitePropertyService sitePropertyService)
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

        [HttpPut]
        public IActionResult Put(SitePropertyUpdateDto dto)
        {
            var result = _sitePropertyService.Update(dto);
            return StatusCode(result.StatusCode, result);
        }
    }
}
