using Entities.Dtos;
using FluentValidation;

namespace Business.ValidationRules.FluentValidation
{
    public class BlogCategoryAddValidation : AbstractValidator<BlogCategoryAddDto>
    {
        public BlogCategoryAddValidation()
        {
            RuleFor(x => x.Name).NotEmpty().WithMessage("Blog Kategori Adı boş bırakılamaz");
        }
    }
}
