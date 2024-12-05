using FluentValidation;

namespace SocialMediaBackend.Application.Features.Comments.Commands.CreateComment
{
    public class CreateCommentCommandValidator : AbstractValidator<CreateCommentCommandRequest>
    {
        public CreateCommentCommandValidator()
        {
            RuleFor(x => x.Content)
                .NotEmpty().WithMessage("Yorum boş olamaz")
                .MaximumLength(500).WithMessage("Yorum en fazla 500 karakter olabilir");
        }
    }
}
