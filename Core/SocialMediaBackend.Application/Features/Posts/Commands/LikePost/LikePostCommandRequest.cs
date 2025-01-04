using MediatR;

namespace SocialMediaBackend.Application.Features.Posts.Commands.LikePost;

public class LikePostCommandRequest : IRequest<LikePostCommandResponse>
{
    public string PostId { get; set; }
    public string UserId { get; set; }
}
