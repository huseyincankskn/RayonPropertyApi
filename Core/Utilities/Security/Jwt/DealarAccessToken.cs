using System;

namespace Core.Utilities.Security.Jwt
{
    public class DealarAccessToken
    {
        public string Token { get; set; }
        public DateTime Expiration { get; set; }
    }
}