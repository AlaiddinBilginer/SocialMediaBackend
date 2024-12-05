using FluentValidation;

namespace SocialMediaBackend.Application.Features.Comments.Commands.UpdateComment
{
    public class UpdateCommentCommandValidator : AbstractValidator<UpdateCommentCommandRequest>
    {
        public UpdateCommentCommandValidator()
        {
            RuleFor(x => x.Content)
                .NotEmpty().WithMessage("Yorum boş olamaz")
                .MaximumLength(500).WithMessage("Yorum en fazla 500 karakter olabilir");
        }
    }
}
