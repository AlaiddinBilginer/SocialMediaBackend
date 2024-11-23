using MediatR;

namespace SocialMediaBackend.Application.Features.Posts.Commands.UpdatePost
{
    public class UpdatePostCommandRequest : IRequest<UpdatePostCommandResponse>
    {
        public string Id { get; set; }
        public string? Title { get; set; }
        public string? Content { get; set; }
        public string CategoryId { get; set; }
    }
}
