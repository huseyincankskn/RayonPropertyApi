using Core.Entities.Dtos;
using Core.Utilities.Security.Jwt;
using Helper.AppSetting;
using Microsoft.AspNetCore.Http;
using Microsoft.IdentityModel.Tokens;
using System;
using System.IdentityModel.Tokens.Jwt;

using System.Text;


namespace Core.Middleware
{
    public class JwtMiddleware
    {
        private readonly RequestDelegate _next;

        public JwtMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext context)
        {
            string token;
            var path = context.Request.Path.ToString().ToLower();

            var authQuery = context.Request.Query["t"];
            if (string.IsNullOrEmpty(context.Request.Headers["Authorization"]) && !string.IsNullOrEmpty(authQuery))
            {
                context.Request.Headers["Authorization"] = $"Bearer {authQuery}";
            }

            var authHeader = context.Request.Headers["Authorization"];
            token = authHeader.ToString().StartsWith("bearer", StringComparison.InvariantCultureIgnoreCase) ? authHeader.FirstOrDefault()?.Split(" ").Last() : null;

            if (token != null)
                AttachAccountToContext(context, token);

            await _next(context);
        }

        private static void AttachAccountToContext(HttpContext context, string token)
        {
            try
            {
                var tokenHandler = new JwtSecurityTokenHandler();
                var key = Encoding.ASCII.GetBytes(AppSettings.SecurityKey);
                tokenHandler.ValidateToken(token, new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(key),
                    ValidateIssuer = false,
                    ValidateAudience = false,
                    ClockSkew = TimeSpan.Zero
                }, out SecurityToken validatedToken);

                var jwtToken = (JwtSecurityToken)validatedToken;
                context.Items["RayonPropertyUser"] = new AuthUserDto()
                {
                    UserId = Guid.Parse(jwtToken.Claims.First(x => x.Type == CustomClaimTypes.UserId).Value),
                    UserName = jwtToken.Claims.First(x => x.Type == CustomClaimTypes.UserName).Value,
                    UserEmail = jwtToken.Claims.First(x => x.Type == CustomClaimTypes.UserEmail).Value,
                };
            }
            catch
            {
                // ignored
            }
        }
    }
}