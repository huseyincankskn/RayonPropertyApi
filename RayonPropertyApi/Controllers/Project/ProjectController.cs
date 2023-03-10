using Entities.Dtos;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Drawing.Imaging;
using System.Drawing;
using Business.Abstract.Project;
using Entities.VMs;
using Microsoft.AspNetCore.OData.Query;
using System.Drawing.Printing;
using Core.Entities.Exceptions;
using Business.Attributes;

namespace RayonPropertyApi.Controllers.Project
{

    [Route("api/[controller]")]
    [ApiController]
    [ApiExplorerSettings(IgnoreApi = false)]
    public class ProjectController : ControllerBase
    {
        private readonly IProjectService _productService;

        public ProjectController(IProjectService productService)
        {
            _productService = productService;
        }
        [EnableQuery(EnsureStableOrdering = false, PageSize = 100)]
        [ProducesResponseType(typeof(ProjectVm), 200)]
        [ProducesResponseType(typeof(object), 403)]
        [ProducesResponseType(typeof(object), 401)]
        [HttpGet("GetList")]
        public IActionResult Get()
        {
            var result = _productService.GetListQueryableOdata();
            if (result.Success)
            {
                return Ok(result.Data);
            }
            return BadRequest(result.Message);
        }
        [ProducesResponseType(typeof(ProjectVm), 200)]
        [ProducesResponseType(typeof(object), 403)]
        [ProducesResponseType(typeof(object), 401)]
        [HttpGet("GetById/{id}")]
        public IActionResult GetById(Guid id)
        {
            var result = _productService.GetById(id);

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
        [RayonPropertyAuthorize]
        [HttpPost]
        public IActionResult AddProject(ProjectDto projectDto)
        {
            var result = _productService.AddProject(projectDto);
            return Ok(result);
        }

        [RayonPropertyAuthorize]
        [HttpPost("SellOrNot")]
        public IActionResult SellOrNot(IsSoldDto dto)
        {
            var result = _productService.SellOrNot(dto);
            return Ok(result);
        }

        [RayonPropertyAuthorize]
        [HttpPut]
        public IActionResult Put(ProjectDto dto)
        {
            var result = _productService.Update(dto);
            return StatusCode(result.StatusCode, result);
        }
        [RayonPropertyAuthorize]
        [HttpPost("AddImage")]
        [RequestFormLimits(ValueLengthLimit = int.MaxValue, MultipartBodyLengthLimit = int.MaxValue)]
        public IActionResult AddImage([FromForm] List<IFormFile> images, [FromForm] string productId)
        {
            var result = _productService.SaveImages(images, productId);
            return Ok(result);
        }
        [RayonPropertyAuthorize]
        [HttpGet("DeletePhoto")]
        public IActionResult DeletePhoto(string fileName)
        {
            var result = _productService.DeletePhoto(fileName);
            return Ok(result);
        }
        [EnableQuery(EnsureStableOrdering = false, PageSize = 100)]
        [ProducesResponseType(typeof(ProjectFeaturesVm), 200)]
        [ProducesResponseType(typeof(object), 403)]
        [ProducesResponseType(typeof(object), 401)]
        //[RayonPropertyAuthorize]
        [HttpGet("GetProjectFeatures")]
        public IActionResult GetProjectFeatures()
        {
            var result = _productService.GetProjectFeatureList();
            if (result.Success)
            {
                return Ok(result.Data);
            }
            return BadRequest(result.Message);
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(Guid id)
        {
            var result = _productService.Delete(id);
            return StatusCode(result.StatusCode, result);
        }
    }
}
