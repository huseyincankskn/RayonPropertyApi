using Business.Abstract;
using Business.Attributes;
using Business.Concrete;
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
        private readonly IRoleService _roleService;

        public UserController(IAuthService authService,
                              IRoleService roleService)
        {
            _authService = authService;
            _roleService = roleService;
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
        public IActionResult UserAdd(UserForRegisterDto userForRegisterDto)
        {
            var userExists = _authService.IsUserExists(new AuthUserDto() { UserEmail = userForRegisterDto.Email });
            if (!userExists.Success)
            {
                return BadRequest("ErrorMail");
            }
            var result = _authService.Add(userForRegisterDto);
            return StatusCode(result.StatusCode, result);
        }

        [HttpPost("UpdateUserRole")]
        [ApiExplorerSettings(IgnoreApi = true)]
        public IActionResult UpdateUserRole(UserRoleDto roleDto)
        {
            var result = _roleService.UpdateUserRoles(roleDto);
            return StatusCode(result.StatusCode, result);
        }

        [HttpPost("RoleList")]
        public IActionResult RoleList(RoleVm model)
        {
            var result = _roleService.RoleList(model);

            if (result.Success)
            {
                return Ok(result);
            }

            return BadRequest(result.Message);
        }

        [HttpPost("IsUserFullPageAuth/{id}")]
        public IActionResult IsUserFullPageAuth(Guid id)
        {
            var result = _authService.IsUserFullPageAuth(id);
            if (result.Success)
            {
                return Ok(result);
            }
            return BadRequest(result.Message);
        }

        [HttpDelete]
        public IActionResult DeleteUser(Guid id)
        {
            var result = _authService.DeleteUser(id);
            if (result.Success)
            {
                return Ok(result);
            }
            return BadRequest(result.Message);
        }
    }
}
