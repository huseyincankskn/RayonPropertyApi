using Entities.Dtos;
using FluentValidation;

namespace Business.ValidationRules.FluentValidation
{
    public class SitePropertyUpdateValidation : AbstractValidator<SitePropertyUpdateDto>
    {
        public SitePropertyUpdateValidation()
        {
            RuleFor(x => x.Email).EmailAddress().MaximumLength(50).NotEmpty().WithMessage("Email maximum length is 50 characters.");
            RuleFor(x => x.Address).MaximumLength(1000).NotEmpty().WithMessage("Address maximum lenght is 1000 and that is required");
            RuleFor(x => x.AddressDe).MaximumLength(1000).NotEmpty().WithMessage("Address German maximum length is 1000 and that is required");
            RuleFor(x => x.AddressRu).MaximumLength(1000).NotEmpty().WithMessage("Address Russian maximum length is 1000 and that is required");
            RuleFor(x => x.PhoneNumber).Length(10).WithMessage("Phone number length must 10 characters");
            RuleFor(x => x.AboutUsText).NotEmpty().MaximumLength(4000).WithMessage("About Us Text maximum length is 4000 characters");
            RuleFor(x => x.AboutUsTextDe).NotEmpty().MaximumLength(4000).WithMessage("About Us German Text maximum length is 4000 characters");
            RuleFor(x => x.AboutUsTextRu).NotEmpty().MaximumLength(4000).WithMessage("About Us Russian Text maximum length is 4000 characters");
        }
    }
}
