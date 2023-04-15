using Entities.Dtos;
using FluentValidation;

namespace Business.ValidationRules.FluentValidation
{
    public class UserAddValidation : AbstractValidator<UserForRegisterDto>
    {
        public UserAddValidation()
        {
            RuleFor(x => x.Email).EmailAddress().MaximumLength(50).NotEmpty().WithMessage("Email maximum length is 50 characters.");
            RuleFor(x => x.FirstName).MaximumLength(250).NotEmpty().WithMessage("First Name maximum length is 250 characters.");
            RuleFor(x => x.LastName).MaximumLength(250).NotEmpty().WithMessage("Last Name maximum length is 250 characters.");
            RuleFor(x => x.PhoneNumber).MaximumLength(10).WithMessage("Phone Number is 10 characters.");
        }
    }
}
