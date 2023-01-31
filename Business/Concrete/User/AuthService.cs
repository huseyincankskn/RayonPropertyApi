﻿using Business.Abstract;
using Core.Utilities.Results;
using DataAccess.Abstract;
using Entities.Concrete;
using Entities.Dtos;
using FluentValidation;
using Business.Constants;
using Core.Utilities.Security.Hashing;
using Core.Utilities.Security.Jwt;
using Core.Entities.Dtos;
using Communication.EmailManager.Abstract;
using Entities.VMs.User;

namespace Business.Concrete
{
    public class AuthService : IAuthService
    {
        private readonly IUserRepository _userRepository;
        private readonly IJwtHelper _tokenHelper;
        private readonly IEmailManager _emailManager;

        public AuthService(IUserRepository userRepository,
                           IJwtHelper tokenHelper,
                           IEmailManager emailManager)
        {
            _userRepository = userRepository;
            _tokenHelper = tokenHelper;
            _emailManager = emailManager;
        }

        public IDataResult<User> Add(UserForRegisterDto userForRegisterDto)
        {
            HashingHelper.CreatePasswordHash(userForRegisterDto.Password, out var passwordHash, out var passwordSalt);
            var user = new User
            {
                Email = userForRegisterDto.Email,
                FirstName = userForRegisterDto.FirstName,
                LastName = userForRegisterDto.LastName,
                PsrHash = passwordHash,
                PsrSalt = passwordSalt,
                PhoneNumber = userForRegisterDto.PhoneNumber,
            };
            _userRepository.Add(user);
            return new SuccessDataResult<User>(user, Messages.UserRegistered);
        }

        public IDataResult<AccessToken> CreateAccessToken(User user)
        {
            var userDto = new UserDto()
            {
                Email = user.Email,
                FirstName = user.FirstName,
                Id = user.Id,
                LastName = user.LastName,
                PhoneNumber = user.PhoneNumber,
            };
            var accessToken = _tokenHelper.CreateToken(userDto);
            return new SuccessDataResult<AccessToken>(accessToken, Messages.AccessTokenCreated);
        }

        public IResult ForgotPassword(ForgotPasswordVm forgotModel)
        {
            var user = _userRepository.GetAllForOdata().FirstOrDefault(x => x.Email == forgotModel.Email.Trim());
            if (user == null)
            {
                throw new ValidationException(Messages.UserNotFound);
            }
            user.PsrGuid = Guid.NewGuid();
            _userRepository.UpdateWithoutLogin(user);

            forgotModel.PsrGuid = user.PsrGuid;

            _emailManager.SendForgotPasswordEmail(forgotModel);
            return new SuccessResult();
        }

        public IResult IsUserExists(AuthUserDto authUserDto)
        {
            var user = _userRepository.GetAllForOdata().FirstOrDefault(x => x.Email == authUserDto.UserEmail);
            if (user != null)
            {
                return new SuccessResult(Messages.UserExist);
            }
            return new ErrorResult();
        }

        public IDataResult<User> Login(UserForLoginDto userForLoginDto)
        {
            var userToCheck = _userRepository.GetAllForOdata().FirstOrDefault(x => x.Email == userForLoginDto.Email);
            if (userToCheck == null)
            {
                return new ErrorDataResult<User>(Messages.UserNotFound);
            }

            if (!HashingHelper.VerifyPasswordHash(userForLoginDto.Password, userToCheck.PsrHash, userToCheck.PsrSalt))
            {
                return new ErrorDataResult<User>(Messages.PasswordError);
            }

            return new SuccessDataResult<User>(userToCheck, Messages.SuccessfulLogin);
        }
    }
}
