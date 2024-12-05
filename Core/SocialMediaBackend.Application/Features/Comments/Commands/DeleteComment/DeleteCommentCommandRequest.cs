using MediatR;

namespace SocialMediaBackend.Application.Features.Comments.Commands.DeleteComment
{
    public class DeleteCommentCommandRequest : IRequest<DeleteCommentCommandResponse>
    {
        public string Id { get; set; }
    }
}
