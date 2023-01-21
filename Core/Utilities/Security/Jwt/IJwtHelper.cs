

using Core.Entities.Dtos;

namespace Core.Utilities.Security.Jwt
{
    public interface IJwtHelper
    {
        AccessToken CreateToken(UserDto user);
    }
}