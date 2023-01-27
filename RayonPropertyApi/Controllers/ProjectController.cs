using Business.Abstract;
using Entities.Dtos;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Drawing.Imaging;
using System.Drawing;

namespace RayonPropertyApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [ApiExplorerSettings(IgnoreApi = false)]
    public class ProjectController : ControllerBase
    {
        private readonly IProductService _productService;

        public ProjectController(IProductService productService)
        {
            _productService = productService;
        }

        [HttpPost]
        public IActionResult AddProject(ProjectDto projectDto)
        {
            return Ok("Test is ok.");
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
