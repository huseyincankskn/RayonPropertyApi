using System;

namespace Core.Utilities.Security.Jwt
{
    public class TokenFirmUserInfo : TokenUserInfo
    {
        public Guid FirmId { get; set; }
        public string FirmName { get; set; }
    }
}