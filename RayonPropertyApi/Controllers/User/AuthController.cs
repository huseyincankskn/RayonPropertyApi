using Business.Abstract;
using Business.Attributes;
using Core.Entities.Dtos;
using Entities.Dtos;
using Entities.VMs;
using Entities.VMs.User;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OData.Query;

namespace RayonPropertyApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [ApiExplorerSettings(IgnoreApi = true)]
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


        [AllowAnonymous]
        [HttpPost("ResetPassword")]
        [ApiExplorerSettings(IgnoreApi = true)]
        public ActionResult ResetPassword(CreatePasswordVm resetPasswordModel)
        {
            var result = _authService.ResetPassword(resetPasswordModel);
            return StatusCode(result.StatusCode, result);
        }

        [AllowAnonymous]
        [HttpGet("IsHavePsrGuid")]
        [ApiExplorerSettings(IgnoreApi = true)]
        public ActionResult IsHavePsrGuid(Guid PsrGuid)
        {
            var result = _authService.IsHavePsrGuid(PsrGuid);
            return StatusCode(result.StatusCode, result);
        }
    }
}
