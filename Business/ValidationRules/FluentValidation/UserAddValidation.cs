using Entities.Dtos;
using FluentValidation;

namespace Business.ValidationRules.FluentValidation
{
    public class UserAddValidation : AbstractValidator<UserForRegisterDto>
    {
        public UserAddValidation()
        {
            RuleFor(x => x.Email).EmailAddress().MaximumLength(50).NotEmpty().WithMessage("Email maksimum 50 karakter olmalıdır");
            RuleFor(x => x.FirstName).MaximumLength(250).NotEmpty().WithMessage("İsim maksimum 1000 karakter olmalıdır");
            RuleFor(x => x.LastName).MaximumLength(250).NotEmpty().WithMessage("Soyisim maksimum 1000 karakter olmalıdır");
            RuleFor(x => x.PhoneNumber).MaximumLength(10).WithMessage("Telefon numarası 10 haneli olmalıdır");
        }
    }
}
