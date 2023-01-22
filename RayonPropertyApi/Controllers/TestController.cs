using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace RayonPropertyApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [ApiExplorerSettings(IgnoreApi = false)]
    public class TestController : ControllerBase
    {

        [HttpGet]
        public IActionResult GetTest()
        {
            return Ok("Test is ok.");
        }
    }
}
