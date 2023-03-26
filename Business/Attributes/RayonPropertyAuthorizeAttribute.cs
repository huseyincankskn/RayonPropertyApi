
using Business.Abstract;
using Core.Entities.Dtos;
using Helper;
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
            var roleService = (IRoleService)context.HttpContext.RequestServices.GetService(typeof(IRoleService));

            if (authService is null || roleService is null)
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

            var user = authService.GetForAuthorization(authUserDto.UserId, authUserDto.UserEmail);
            if (user == null)
            {
                context.Result = new StatusCodeResult(401);
                return;
            }

            if (user.Data.IsAdmin)
            {
                return;
            }

            var requestMethod = context.HttpContext.Request.Method;
            var requestPath = context.HttpContext.Request.Path.ToString().GetRequestRolePath();

            if (requestPath == "Dashboard")
            {
                return;
            }
            var requestRole = roleService.GetRoleByPath(requestPath, requestMethod);
            if (requestRole == null)
            {
                context.Result = new StatusCodeResult(403);
                return;
            }

            var checkUserRole = roleService.CheckUserRole(user.Data.Id, requestRole.Id);
            if (checkUserRole == null)
            {
                context.Result = new StatusCodeResult(403);
                return;
            }
        }
    }
}