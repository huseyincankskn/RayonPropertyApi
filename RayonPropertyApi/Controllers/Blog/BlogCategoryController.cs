using Business.Abstract;
using Business.Attributes;
using Core.Entities.Exceptions;
using Entities.Dtos;
using Entities.VMs;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OData.Query;


namespace RayonPropertyApi.Controllers.Blog
{
    [Route("api/[controller]")]
    [ApiController]
    [RayonPropertyAuthorize]
    [ApiExplorerSettings(IgnoreApi = true)]
    public class BlogCategoryController : ControllerBase
    {
        private readonly IBlogCategoryService _blogCategoryService;

        public BlogCategoryController(IBlogCategoryService blogCategoryService)
        {
            _blogCategoryService = blogCategoryService;
        }



        [EnableQuery(EnsureStableOrdering = false, PageSize = 100)]
        [ProducesResponseType(typeof(BlogVm), 200)]
        [ProducesResponseType(typeof(object), 403)]
        [ProducesResponseType(typeof(object), 401)]
        [HttpGet]
        public IActionResult GetList()
        {
            var result = _blogCategoryService.GetListQueryableOdata();
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

        [HttpPost]
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
