using Business.Abstract;
using Business.Attributes;
using Core.Entities.Exceptions;
using Entities.Dtos;
using Entities.VMs;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OData.Query;
using Microsoft.AspNetCore.OData.Routing.Controllers;

namespace RayonPropertyApi.Controllers.BlogCategory
{
    [Route("api/[controller]")]
    [ApiController]
    [RayonPropertyAuthorize]
    [ApiExplorerSettings(IgnoreApi = true)]
    public class BlogCategoryController : ODataController
    {
        private readonly IBlogCategoryService _blogCategoryService;

        public BlogCategoryController(IBlogCategoryService blogCategoryService)
        {
            _blogCategoryService = blogCategoryService;
        }

        [EnableQuery]
        [HttpGet("GetList")]
        public IActionResult Get()
        {
            var result = _blogCategoryService.GetListQueryableOdata();
            if (result.Success)
            {
                return Ok(result.Data);
            }
            return BadRequest(result.Message);
        }

        [ProducesResponseType(typeof(BlogCategoryVm), 200)]
        [ProducesResponseType(typeof(object), 403)]
        [ProducesResponseType(typeof(object), 401)]
        [HttpGet("GetById/{id}")]
        public IActionResult GetById(Guid id)
        {
            var result = _blogCategoryService.GetById(id);

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

        [HttpPost("CategoryAdd")]
        public IActionResult Post(BlogCategoryAddDto dto)
        {
            var result = _blogCategoryService.Add(dto);
            return StatusCode(result.StatusCode, result);
        }

        [HttpPut]
        public IActionResult Put(BlogCategoryUpdateDto dto)
        {
            var result = _blogCategoryService.Update(dto);
            return StatusCode(result.StatusCode, result);
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(Guid id)
        {
            var result = _blogCategoryService.Delete(id);
            return StatusCode(result.StatusCode, result);
        }
    }
}
