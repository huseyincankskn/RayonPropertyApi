using Entities.Dtos;
using FluentValidation;

namespace Business.ValidationRules.FluentValidation
{
    public class CommentUpdateValidation : AbstractValidator<CommentUpdateDto>
    {
        public CommentUpdateValidation()
        {
            RuleFor(x => x.Name).NotEmpty().WithMessage("Name is required");
            RuleFor(x => x.Name).MaximumLength(100).WithMessage("Name maximum length 100 characters.");
            RuleFor(x => x.CommentText).NotEmpty().WithMessage("Comment Text is required");
            RuleFor(x => x.CommentText).MaximumLength(3000).WithMessage("Comment Text maximum length 3000 characters");
            RuleFor(x => x.Country).NotEmpty().WithMessage("Country is required");
            RuleFor(x => x.Country).MaximumLength(100).WithMessage("Country maximum length 100 characters.");
            RuleFor(x => x.CommentTextDe).NotEmpty().WithMessage("Comment Text German is required");
            RuleFor(x => x.CommentTextDe).MaximumLength(3000).WithMessage("Comment Text German maximum length 3000 characters");
            RuleFor(x => x.CommentTextRu).NotEmpty().WithMessage("Comment Text Russian is required");
            RuleFor(x => x.CommentTextRu).MaximumLength(3000).WithMessage("Comment Text Russian maximum length 3000 characters");
        }
    }
}
