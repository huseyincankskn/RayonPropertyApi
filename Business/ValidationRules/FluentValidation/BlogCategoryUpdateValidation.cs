using Entities.Dtos;
using FluentValidation;

namespace Business.ValidationRules.FluentValidation
{
    public class BlogCategoryUpdateValidation : AbstractValidator<BlogCategoryUpdateDto>
    {
        public BlogCategoryUpdateValidation()
        {
            RuleFor(x => x.Name).NotEmpty().WithMessage("Blog Kategori Adı boş bırakılamaz");
        }
    }
}
