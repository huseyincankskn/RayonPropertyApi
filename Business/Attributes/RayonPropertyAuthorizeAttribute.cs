
using Core.Entities.Dtos;
using Helper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Linq;


namespace Business.Attributes
{
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method)]
    public class RayonPropertyAuthorizeAttribute : Attribute, IAuthorizationFilter
    {
        public void OnAuthorization(AuthorizationFilterContext context)
        {
            var rayonProperty = (AuthUserDto)context.HttpContext.Items["RayonPropertyUser"];
            if (rayonProperty == null)
            {
                context.Result = new StatusCodeResult(401);
                return;
            }

            return;

            //var roleService = (IRoleService)context.HttpContext.RequestServices.GetService(typeof(IRoleService));
            //var tenantUserService = (ITenantUserService)context.HttpContext.RequestServices.GetService(typeof(ITenantUserService));
            //var tenantService = (ITenantService)context.HttpContext.RequestServices.GetService(typeof(ITenantService));

            //var requestMethod = context.HttpContext.Request.Method;
            //var requestPath = context.HttpContext.Request.Path.ToString().GetRequestRolePath();

            //var tenantUser = tenantUserService.GetForAuthorization(paraticUser.UserId, paraticUser.TenantId);
            //if (tenantUser == null)
            //{
            //    context.Result = new StatusCodeResult(401);
            //    return;
            //}

            //var requestRole = roleService.GetRoleByPath(requestPath, requestMethod);
            //if (requestRole == null)
            //{
            //    //todo veritabanına roller tanımlandıktan sonra kaldırılacak.
            //    if (tenantUser.IsAdmin || tenantUser.IsFullPageAuth)
            //        return;

            //    context.Result = new StatusCodeResult(403);
            //    return;
            //}

            //if (!requestRole.IsLoginRequired)
            //    return;

            //var tenantModules = tenantService.GetTenantModules(paraticUser.TenantId);
            //var tenantModuleIds = tenantModules.Select(x => x.Id).ToList();
            //if (!tenantModuleIds.Contains(requestRole.ModuleId))
            //{
            //    context.Result = new StatusCodeResult(403);
            //    return;
            //}

            //if (tenantUser.IsAdmin || tenantUser.IsFullPageAuth)
            //    return;

            //var tenantUserRoles = tenantUserService.GetTenantUserRoles(tenantUser.Id);
            //var tenantUserRoleIds = tenantUserRoles.Select(x => x.Id).ToList();
            //if (!tenantUserRoleIds.Contains(requestRole.Id))
            //{
            //    context.Result = new StatusCodeResult(403);
            //}
        }
    }
}