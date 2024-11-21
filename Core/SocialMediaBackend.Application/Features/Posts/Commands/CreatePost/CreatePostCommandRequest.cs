using MediatR;

namespace SocialMediaBackend.Application.Features.Posts.Commands.CreatePost
{
    public class CreatePostCommandRequest : IRequest<CreatePostCommandResponse>
    {
        public string? Title { get; set; }
        public string? Content { get; set; }
        public string CategoryId { get; set; } = string.Empty;
    }
}
