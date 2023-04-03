using Entities.Dtos;
using FluentValidation;

namespace Business.ValidationRules.FluentValidation
{
    public class BlogUpdateValidation : AbstractValidator<BlogUpdateDto>
    {
        public BlogUpdateValidation()
        {
            RuleFor(x => x.Title).NotEmpty().WithMessage("Title is required.");
            RuleFor(x => x.Title).MaximumLength(200).WithMessage("Title maximum length is 200 characters.");

            RuleFor(x => x.TitleDe).NotEmpty().WithMessage("Title German is required.");
            RuleFor(x => x.TitleDe).MaximumLength(200).WithMessage("Title Russian maximum length is 200 characters.");

            RuleFor(x => x.TitleRu).NotEmpty().WithMessage("Title Russian is required");
            RuleFor(x => x.TitleRu).MaximumLength(200).WithMessage("Title Russian maximum length is 200 characters.");

            RuleFor(x => x.Post).NotEmpty().WithMessage("Post is required.");
            RuleFor(x => x.Post).MaximumLength(4000).WithMessage("Post maximum length is 4000 characters.");

            RuleFor(x => x.PostDe).NotEmpty().WithMessage("Post German is required.");
            RuleFor(x => x.PostDe).MaximumLength(4000).WithMessage("Post German maximum length is 4000 characters.");

            RuleFor(x => x.PostRu).NotEmpty().WithMessage("Post Russian is required.");
            RuleFor(x => x.PostRu).MaximumLength(4000).WithMessage("Post Russian maximum length is 4000 characters.");
        }
    }
}
