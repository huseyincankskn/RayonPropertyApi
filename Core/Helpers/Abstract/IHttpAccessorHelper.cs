using Core.Utilities.Security.Jwt;
using Microsoft.AspNetCore.Http;
using System;

namespace Core.Helpers
{
    public interface IHttpAccessorHelper
    {
        Guid GetUserId();

        string GetUserEMail();

        string GetUserName();

        string GetQueryString(string query);

        string GetAllQueryString();

        string GetJwtClaim(string claimType);

        HttpContext GetHttpContext();

        string GetHeader(string key);

        TokenUserInfo GetUserInfo();

        string GetCurrentApiBaseUrl();
    }
}