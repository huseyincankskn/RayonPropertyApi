using Business.Abstract;
using Business.Attributes;
using Entities.Dtos;
using Entities.VMs.User;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace RayonPropertyApi.Controllers.User
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _authService;

        public AuthController(IAuthService authService)
        {
            _authService = authService;
        }

        [AllowAnonymous]
        [HttpPost("Login")]
        [ApiExplorerSettings(IgnoreApi = true)]
        public IActionResult Login(UserForLoginDto userForLoginDto)
        {
            var userToLogin = _authService.Login(userForLoginDto);
            if (!userToLogin.Success)
            {
                return BadRequest(userToLogin.Message);
            }
            else if (userToLogin.Success && userToLogin.Data is null)
            {
                return Ok(userToLogin);
            }

            var result = _authService.CreateAccessToken(userToLogin.Data);
            return StatusCode(result.StatusCode, result);
        }

        [AllowAnonymous]
        [HttpPost("ForgotPassword")]
        [ApiExplorerSettings(IgnoreApi = true)]
        public ActionResult ForgotPassword(ForgotPasswordVm forgotModel)
        {
            var result = _authService.ForgotPassword(forgotModel);
            return StatusCode(result.StatusCode, result);
        }
    }
}
