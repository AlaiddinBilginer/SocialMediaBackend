using MediatR;

namespace SocialMediaBackend.Application.Features.Categories.Commands.CreateCategory
{
    public class CreateCategoryCommandRequest : IRequest<CreateCategoryCommandResponse>
    {
        public string Title { get; set; } = string.Empty;
        public string? Photo { get; set; }
    }
}
