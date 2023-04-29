using Business.Abstract;
using Business.Concrete;
using Core.Entities.Exceptions;
using Entities.VMs;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.IO;


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
        private readonly ITranslateService _translateService;
        private readonly ICurrencyService _currencyService;

        public WebSiteInfoController(ISitePropertyService sitePropertyService,
                                     IBlogService blogService,
                                     IBlogCategoryService blogCategoryService,
                                     ITranslateService translateService,
                                     ICurrencyService currencyService)
        {
            _sitePropertyService = sitePropertyService;
            _blogService = blogService;
            _blogCategoryService = blogCategoryService;
            _translateService = translateService;
            _currencyService = currencyService;
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
        [HttpPost("GetProjectCount")]
        public IActionResult GetProjectCount(List<int> idList)
        {
            var result = _sitePropertyService.GetProjectCount(idList);
            return Ok(result.Data);
        }

        [HttpGet("GetTranslate")]
        public IActionResult GetTranslate()
        {
            var result = _translateService.GetList();
            if (result.Success)
            {
                return Ok(result.Data);
            }
            return BadRequest(result.Message);
        }
        
        [HttpGet("GetTranslateDictionary/{locale}")]
        public IActionResult GetTranslations(string locale)
        {
            var result = _translateService.GetTranslateDictionary(locale);
            if (result.Success)
            {
                return Ok(result.Data);
            }
            return BadRequest(result.Message);
        }

        [HttpGet("GetCurrencyRate")]
        public IActionResult GetCurrencyRate()
        {
            var result = _currencyService.GetCurrencyRates();
            if (result.Success)
            {
                return Ok(result.Data);
            }
            return BadRequest(result.Message);
        }
    }
}
