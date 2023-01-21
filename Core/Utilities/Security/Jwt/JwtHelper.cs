using Core.Entities.Dtos;
using Core.Extensions;
using Core.Utilities.Security.Encryption;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace Core.Utilities.Security.Jwt
{
    public class JwtHelper : IJwtHelper
    {
        private readonly TokenOptions _tokenOptions;
        private DateTime _accessTokenExpiration;

        public JwtHelper(IConfiguration configuration)
        {
            _tokenOptions = configuration.GetSection("TokenOptions").Get<TokenOptions>();
        }

        public AccessToken CreateToken(UserDto user)
        {
            _accessTokenExpiration = DateTime.Now.AddMinutes(_tokenOptions.AccessTokenExpiration);
            var securityKey = SecurityKeyHelper.CreateSecurityKey(_tokenOptions.SecurityKey);
            var signingCredentials = SigningCredentialsHelper.CreateSigningCredentials(securityKey);
            var jwt = CreateJwtSecurityToken(_tokenOptions, user, signingCredentials);
            var jwtSecurityTokenHandler = new JwtSecurityTokenHandler();
            var token = jwtSecurityTokenHandler.WriteToken(jwt);

            var accessToken = new AccessToken
            {
                Token = token,
                Expiration = _accessTokenExpiration,
                Email = user.Email,
                UserName = $"{user.FirstName} {user.LastName}",
                UserId = user.Id,
                MobilePhone = user.MobileNumber
            };

            var refreshTokenExpiration = DateTime.Now.AddMinutes(_tokenOptions.RefeshTokenExpiration);
            var refreshJwt = CreateJwtRefreshToken(_tokenOptions, user, signingCredentials, refreshTokenExpiration);

            accessToken.RefreshToken = jwtSecurityTokenHandler.WriteToken(refreshJwt);
            accessToken.RefreshTokenExpiration = refreshTokenExpiration;

            return accessToken;
        }

        public JwtSecurityToken CreateJwtSecurityToken(TokenOptions tokenOptions, UserDto user, SigningCredentials signingCredentials)
        {
            return new JwtSecurityToken(
                issuer: tokenOptions.Issuer,
                audience: tokenOptions.Audience,
                expires: _accessTokenExpiration,
                notBefore: DateTime.Now,
                claims: SetClaims(user),
                signingCredentials: signingCredentials
            );
        }

        public JwtSecurityToken CreateJwtRefreshToken(TokenOptions tokenOptions, UserDto user, SigningCredentials signingCredentials, DateTime expiration)
        {
            return new JwtSecurityToken(
                issuer: tokenOptions.Issuer,
                audience: tokenOptions.Audience,
                expires: expiration,
                notBefore: DateTime.Now,
                claims: SetRefreshTokenClaims(user),
                signingCredentials: signingCredentials
            );
        }

        private static IEnumerable<Claim> SetClaims(UserDto user)
        {
            var claims = new List<Claim>();
            claims.AddUserId(user.Id.ToString());
            claims.AddUserEmail(user.Email);
            claims.AddUserName($"{user.FirstName} {user.LastName}");
            return claims;
        }

        private static IEnumerable<Claim> SetRefreshTokenClaims(UserDto user)
        {
            var claims = new List<Claim>();
            claims.AddUserId(user.Id.ToString());
            return claims;
        }
    }
}