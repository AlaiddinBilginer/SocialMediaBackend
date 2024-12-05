using MediatR;

namespace SocialMediaBackend.Application.Features.Comments.Commands.CreateComment
{
    public class CreateCommentCommandRequest : IRequest<CreateCommentCommandResponse>
    {
        public string PostId { get; set; }
        public string Content { get; set; }
    }
}
