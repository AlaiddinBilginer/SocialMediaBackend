using MediatR;

namespace SocialMediaBackend.Application.Features.Posts.Commands.DeletePost
{
    public class DeletePostCommandRequest : IRequest<DeletePostCommandResponse>
    {
        public string Id { get; set; }
    }
}
