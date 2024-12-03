using FluentValidation;

namespace SocialMediaBackend.Application.Features.Categories.Commands.CreateCategory
{
    public class CreateCategoryCommandValidator : AbstractValidator<CreateCategoryCommandRequest>
    {
        public CreateCategoryCommandValidator()
        {
            RuleFor(x => x.Title)
                .NotEmpty().WithMessage("Kategori boş olamaz")
                .MinimumLength(3).WithMessage("Kategori başlığı en az 3 karakterli olmalıdır")
                .MaximumLength(50).WithMessage("Kategori başlığı en fazla 50 karakterli olmalıdır");
        }
    }
}
