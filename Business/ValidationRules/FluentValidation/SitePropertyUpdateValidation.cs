using Entities.Dtos;
using FluentValidation;

namespace Business.ValidationRules.FluentValidation
{
    public class SitePropertyUpdateValidation : AbstractValidator<SitePropertyUpdateDto>
    {
        public SitePropertyUpdateValidation()
        {
            RuleFor(x => x.Email).EmailAddress().MaximumLength(50).NotEmpty().WithMessage("Email maksimum 50 karakter olmalıdır");
            RuleFor(x => x.Address).MaximumLength(1000).NotEmpty().WithMessage("Adres maksimum 1000 karakter olmalıdır");
            RuleFor(x => x.PhoneNumber).Length(10).WithMessage("Telefon numarası 10 haneli olmalıdır");
        }
    }
}
