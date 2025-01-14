using MediatR;

namespace SocialMediaBackend.Application.Features.Comments.Commands.LikeComment;

public class LikeCommentCommandRequest : IRequest<LikeCommentCommandResponse>
{
    public string CommentId { get; set; }
}
