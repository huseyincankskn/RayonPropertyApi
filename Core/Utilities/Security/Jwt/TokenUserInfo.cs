using System;

namespace Core.Utilities.Security.Jwt
{
    public class TokenUserInfo
    {
        public Guid UserId { get; set; }
        public string UserName { get; set; }
        public string Email { get; set; }
    }
}