using Core.Utilities.Results;
using Core.Utilities.Security.Jwt;
using Entities.Concrete;
using Entities.Dtos;
using Entities.VMs.User;

namespace Business.Abstract
{
    public interface IAuthService
    {
        IDataResult<User> Login(UserForLoginDto userForLoginDto);
        IDataResult<AccessToken> CreateAccessToken(User user);
        IResult ForgotPassword(ForgotPasswordVm forgotModel);
    }
}
