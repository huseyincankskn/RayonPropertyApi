using System;

namespace Core.Entities.Dtos
{
    public class AuthUserDto
    {
        public string? UserEmail { get; set; }
        public string? UserName { get; set; }
        public Guid UserId { get; set; }
    }
}