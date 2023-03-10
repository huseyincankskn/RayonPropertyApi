using Business.Abstract;
using Business.Attributes;
using Business.Concrete;
using Core.Entities.Exceptions;
using Entities.Dtos;
using Entities.VMs;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OData.Query;
using Microsoft.AspNetCore.OData.Routing.Controllers;

namespace RayonPropertyApi.Controllers.Comment
{
    [RayonPropertyAuthorize]
    [Route("api/[controller]")]
    [ApiController]
    [ApiExplorerSettings(IgnoreApi = true)]
    public class CommentController : ODataController
    {
        private readonly ICommentService _commentService;

        public CommentController(ICommentService commentService)
        {
            _commentService = commentService;
        }


        [EnableQuery(EnsureStableOrdering = false, PageSize = 100)]
        [ProducesResponseType(typeof(CommentVm), 200)]
        [ProducesResponseType(typeof(object), 403)]
        [ProducesResponseType(typeof(object), 401)]
        [HttpGet("GetList")]
        public IActionResult Get()
        {
            var result = _commentService.GetListQueryableOdata();
            if (result.Success)
            {
                return Ok(result.Data);
            }
            return BadRequest(result.Message);
        }

        [ProducesResponseType(typeof(CommentVm), 200)]
        [ProducesResponseType(typeof(object), 403)]
        [ProducesResponseType(typeof(object), 401)]
        [HttpGet("GetById/{id}")]
        public IActionResult GetById(Guid id)
        {
            var result = _commentService.GetById(id);

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
        public IActionResult Post(CommentAddDto dto)
        {
            var result = _commentService.Add(dto);
            return StatusCode(result.StatusCode, result);
        }

        [HttpPut]
        public IActionResult Put(CommentUpdateDto dto)
        {
            var result = _commentService.Update(dto);
            return StatusCode(result.StatusCode, result);
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(Guid id)
        {
            var result = _commentService.Delete(id);
            return StatusCode(result.StatusCode, result);
        }
    }
}
