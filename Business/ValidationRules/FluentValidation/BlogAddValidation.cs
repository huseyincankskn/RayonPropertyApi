using Entities.Dtos;
using FluentValidation;

namespace Business.ValidationRules.FluentValidation
{
    public class BlogAddValidation : AbstractValidator<BlogAddDto>
    {
        public BlogAddValidation()
        {
            RuleFor(x => x.Title).NotEmpty().WithMessage("Yazı başlığı boş bırakılamaz");
            RuleFor(x => x.Title).MaximumLength(200).WithMessage("Yazı başlığı 200 karakterden fazla olamaz");
            RuleFor(x => x.Post).NotEmpty().WithMessage("Yazı boş bırakılamaz");
            RuleFor(x => x.Post).MaximumLength(4000).WithMessage("Yazı maksimum 4000 karakter olmalıdır");
        }
    }
}
