using Business.Abstract;
using Business.Attributes;
using Core.Entities.Exceptions;
using Entities.Dtos;
using Entities.VMs;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OData.Query;

namespace RayonPropertyApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [RayonPropertyAuthorize]
    [ApiExplorerSettings(IgnoreApi = true)]
    public class BlogController : ControllerBase
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
        [HttpGet]
        public IActionResult GetList()
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

        [HttpPost]
        public IActionResult Post(BlogAddDto dto)
        {
            var result = _blogService.Add(dto);
            return StatusCode(result.StatusCode, result);
        }

        [HttpPut]
        public IActionResult Put(BlogUpdateDto dto)
        {
            var result = _blogService.Update(dto);
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
