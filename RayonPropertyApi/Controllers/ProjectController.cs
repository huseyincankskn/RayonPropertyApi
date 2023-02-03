using Entities.Dtos;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Drawing.Imaging;
using System.Drawing;
using Business.Abstract.Project;

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
