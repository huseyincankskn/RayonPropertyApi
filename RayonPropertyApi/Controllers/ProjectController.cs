using Business.Abstract;
using Entities.Dtos;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

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
    }
}
