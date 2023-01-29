using Entities.Dtos;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.ValidationRules.FluentValidation
{
    public class BlogUpdateValidation : AbstractValidator<BlogUpdateDto>
    {
        public BlogUpdateValidation()
        {
            RuleFor(x => x.Title).NotEmpty().WithMessage("Yazı başlığı boş bırakılamaz");
            RuleFor(x => x.Title).MaximumLength(200).WithMessage("Yazı başlığı 200 karakterden fazla olamaz");
            RuleFor(x => x.Post).NotEmpty().WithMessage("Yazı boş bırakılamaz");
            RuleFor(x => x.Post).MaximumLength(5000).WithMessage("Yazı maksimum 5000 karakter olmalıdır");
        }
    }
}
