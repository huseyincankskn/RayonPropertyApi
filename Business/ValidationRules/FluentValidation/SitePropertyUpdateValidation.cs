using Entities.Dtos;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.ValidationRules.FluentValidation
{
    public class SitePropertyUpdateValidation : AbstractValidator<SitePropertyUpdateDto>
    {
        public SitePropertyUpdateValidation()
        {
            RuleFor(x => x.Email).MaximumLength(100).NotEmpty().WithMessage("Email maksimum 50 karakter olmalıdır");
            RuleFor(x => x.Name).MaximumLength(200).NotEmpty().WithMessage("Site adı maksimum 200 karakter olmalıdır");
            RuleFor(x => x.Address).MaximumLength(1000).NotEmpty().WithMessage("Adres maksimum 1000 karakter olmalıdır");
            RuleFor(x => x.PhoneNumber).MaximumLength(10).NotEmpty().WithMessage("Telefon 10 haneli olmalıdır");
        }
    }
}
