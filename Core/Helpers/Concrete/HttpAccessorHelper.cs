using Core.Utilities.Security.Jwt;
using Microsoft.AspNetCore.Http;
using System;
using System.Linq;

namespace Core.Helpers
{
    public class HttpAccessorHelper : IHttpAccessorHelper
    {
        private readonly IHttpContextAccessor _httpContextAccessor;

        public HttpAccessorHelper(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }

        public Guid GetUserId()
        {
            var userId = GetJwtClaim(CustomClaimTypes.UserId);
            return string.IsNullOrEmpty(userId) ? Guid.Empty : Guid.Parse(userId);
        }

        public string GetUserEMail()
        {
            return GetJwtClaim(CustomClaimTypes.UserEmail);
        }

        public string GetUserName()
        {
            return GetJwtClaim(CustomClaimTypes.UserName);
        }

        public string GetQueryString(string query)
        {
            return _httpContextAccessor.HttpContext?.Request.Query[query].ToString();
        }

        public string GetAllQueryString()
        {
            return _httpContextAccessor.HttpContext?.Request.QueryString.Value;
        }

        public string GetJwtClaim(string claimType)
        {
            return _httpContextAccessor.HttpContext?.User.Claims.FirstOrDefault(x => x.Type == claimType)?.Value;
        }

        public HttpContext GetHttpContext()
        {
            return _httpContextAccessor.HttpContext;
        }

        public string GetHeader(string key)
        {
            return _httpContextAccessor.HttpContext?.Request.Headers[key].FirstOrDefault();
        }

        public TokenUserInfo GetUserInfo()
        {
            var userId = GetJwtClaim(CustomClaimTypes.UserId);
            return userId == null ? null : new TokenUserInfo
            {
                UserId = Guid.Parse(userId),
                UserName = GetJwtClaim(CustomClaimTypes.UserName),
                Email = GetJwtClaim(CustomClaimTypes.UserEmail),
            };
        }

        public string GetCurrentApiBaseUrl()
        {
            var request = _httpContextAccessor.HttpContext.Request;
            return $"{request.Scheme}://{request.Host}";
        }
    }
}