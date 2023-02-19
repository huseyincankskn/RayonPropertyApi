using Business.Abstract;
using Business.Attributes;
using Core.Entities.Dtos;
using Entities.Dtos;
using Entities.VMs;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OData.Query;
using Microsoft.AspNetCore.OData.Routing.Controllers;


namespace RayonPropertyApi.Controllers
{
    [RayonPropertyAuthorize]
    [Route("api/[controller]")]
    [ApiController]
    [ApiExplorerSettings(IgnoreApi = true)]
    public class UserController : ODataController
    {
        private readonly IAuthService _authService;

        public UserController(IAuthService authService)
        {
            _authService = authService;
        }

        
        [EnableQuery(EnsureStableOrdering = false, PageSize = 100)]
        [ProducesResponseType(typeof(UserVm), 200)]
        [ProducesResponseType(typeof(object), 403)]
        [ProducesResponseType(typeof(object), 401)]
        [HttpGet("GetList")]
        public IActionResult Get()
        {
            var result = _authService.GetAllData();
            if (result.Success)
            {
                return Ok(result.Data);
            }
            return BadRequest(result.Message);
        }

        
        [HttpPost("UserAdd")]
        public ActionResult UserAdd(UserForRegisterDto userForRegisterDto)
        {
            var userExists = _authService.IsUserExists(new AuthUserDto() { UserEmail = userForRegisterDto.Email });
            if (!userExists.Success)
            {
                return BadRequest("ErrorMail");
            }
            var result = _authService.Add(userForRegisterDto);
            return StatusCode(result.StatusCode, result);
        }
    }
}
