using Business.Abstract;
using Business.Concrete;
using Core.Entities.Exceptions;
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
        private readonly IBlogService _blogService;
        public readonly IBlogCategoryService _blogCategoryService;

        public WebSiteInfoController(ISitePropertyService sitePropertyService,
                                     IBlogService blogService,
                                     IBlogCategoryService blogCategoryService)
        {
            _sitePropertyService = sitePropertyService;
            _blogService = blogService;
            _blogCategoryService = blogCategoryService;
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

        [HttpGet("GetBlogList")]
        public IActionResult GetBlogList()
        {
            var result = _blogService.GetListForWebSite();
            if (result.Success)
            {
                return Ok(result.Data);
            }
            return BadRequest(result.Message);
        }

        [HttpGet("GetBlogCategoryList")]
        public IActionResult GetBlogCategoryList()
        {
            var result = _blogCategoryService.GetListForWebSite();
            if (result.Success)
            {
                return Ok(result.Data);
            }
            return BadRequest(result.Message);
        }

        [HttpGet("GetBlogById/{id}")]
        public IActionResult GetBlogById(Guid id)
        {
            var result = _blogService.GetByIdForWebSite(id);

            if (result.Data == null)
            {
                throw new NotFoundException(id);
            }
            if (result.Success)
            {
                return Ok(result.Data);
            }
            return BadRequest(result.Message);
        }
    }
}
