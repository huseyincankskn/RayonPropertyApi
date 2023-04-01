using Entities.Dtos;
using FluentValidation;

namespace Business.ValidationRules.FluentValidation
{
    public class BlogCategoryAddValidation : AbstractValidator<BlogCategoryAddDto>
    {
        public BlogCategoryAddValidation()
        {
            RuleFor(x => x.Name).NotEmpty().WithMessage("Blog Category German Name required");
            RuleFor(x => x.NameDe).NotEmpty().WithMessage("Blog Category German Name is required");
            RuleFor(x => x.NameRu).NotEmpty().WithMessage("Blog Category Russian is required");
        }
    }
}
