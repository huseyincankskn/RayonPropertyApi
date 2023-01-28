
using Business.Abstract;
using Core.Entities.Dtos;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace Business.Attributes
{
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method)]
    public class RayonPropertyAuthorizeAttribute : Attribute, IAuthorizationFilter
    {
        public void OnAuthorization(AuthorizationFilterContext context)
        {
            var rayonPropertyUser = (AuthUserDto)context.HttpContext.Items["RayonPropertyUser"];
            if (rayonPropertyUser == null)
            {
                context.Result = new StatusCodeResult(401);
                return;
            }

            var authService = (IAuthService)context.HttpContext.RequestServices.GetService(typeof(IAuthService));
            if (authService == null)
            {
                context.Result = new StatusCodeResult(500);
                return;
            }

            var authUserDto = new AuthUserDto()
            {
                UserEmail = rayonPropertyUser.UserEmail,
                UserId = rayonPropertyUser.UserId,
                UserName = rayonPropertyUser.UserName
            };

            var isuserExist = authService.IsUserExists(authUserDto);
            if (!isuserExist.Success)
            {
                context.Result = new StatusCodeResult(401);
                return;
            }
        }
    }
}