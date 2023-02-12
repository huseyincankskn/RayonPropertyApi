using Business.Abstract;
using Business.Attributes;
using Core.Entities.Exceptions;
using Entities.Dtos;
using Entities.VMs;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OData.Query;
using Microsoft.AspNetCore.OData.Routing.Controllers;
using Newtonsoft.Json;


namespace RayonPropertyApi.Controllers.Blog
{
    [RayonPropertyAuthorize]
    [Route("api/[controller]")]
    [ApiController]
    [ApiExplorerSettings(IgnoreApi = true)]
    public class BlogController : ODataController
    {
        private readonly IBlogService _blogService;

        public BlogController(IBlogService blogService)
        {
            _blogService = blogService;
        }

        [EnableQuery(EnsureStableOrdering = false, PageSize = 100)]
        [ProducesResponseType(typeof(BlogVm), 200)]
        [ProducesResponseType(typeof(object), 403)]
        [ProducesResponseType(typeof(object), 401)]
        [HttpGet("GetList")]
        public IActionResult Get()
        {
            var result = _blogService.GetListQueryableOdata();
            if (result.Success)
            {
                return Ok(result.Data);
            }
            return BadRequest(result.Message);
        }

        [ProducesResponseType(typeof(BlogVm), 200)]
        [ProducesResponseType(typeof(object), 403)]
        [ProducesResponseType(typeof(object), 401)]
        [HttpGet("GetById/{id}")]
        public IActionResult GetById(Guid id)
        {
            var result = _blogService.GetById(id);

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

        [HttpPost("BlogAdd")]
        public IActionResult Post([FromForm] IFormFile file, [FromForm] string jsonString)
        {
            BlogAddDto dto = JsonConvert.DeserializeObject<BlogAddDto>(jsonString);
            var result = _blogService.Add(file, dto);
            return StatusCode(result.StatusCode, result);
        }

        [HttpPut]
        public IActionResult Put([FromForm] IFormFile? file, [FromForm] string jsonString)
        {
            BlogUpdateDto dto = JsonConvert.DeserializeObject<BlogUpdateDto>(jsonString);
            var result = _blogService.Update(file, dto);
            return StatusCode(result.StatusCode, result);
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(Guid id)
        {
            var result = _blogService.Delete(id);
            return StatusCode(result.StatusCode, result);
        }
    }
}
