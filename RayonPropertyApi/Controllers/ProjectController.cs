using Entities.Dtos;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Drawing.Imaging;
using System.Drawing;
using Business.Abstract.Project;
using Entities.VMs;
using Microsoft.AspNetCore.OData.Query;
using System.Drawing.Printing;

namespace RayonPropertyApi.Controllers
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
        [ProducesResponseType(typeof(BlogVm), 200)]
        [ProducesResponseType(typeof(object), 403)]
        [ProducesResponseType(typeof(object), 401)]
        [HttpGet]
        public IActionResult GetList()
        {
            var result = _productService.GetListQueryableOdata();
            if (result.Success)
            {
                return Ok(result.Data);
            }
            return BadRequest(result.Message);
        }
        [HttpPost]
        public IActionResult AddProject(ProjectDto projectDto)
        {
            var result = _productService.AddProject(projectDto);
            return Ok(result);
        }
        [HttpPost("AddImage")]
        [RequestFormLimits(ValueLengthLimit = int.MaxValue, MultipartBodyLengthLimit = int.MaxValue)]
        public IActionResult AddImage([FromForm] List<IFormFile> images)
        {
            var result = _productService.SaveImages(images);
            return Ok(result);
        }
    }
}
