using SocialMediaBackend.Application.DTOs.Comments;

namespace SocialMediaBackend.Application.Features.Comments.Queries.GetRepliesByParentCommentId
{
    public class GetRepliesByParentCommentIdQueryResponse
    {
        public IQueryable<ReplyCommentDto> Replies { get; set; }
    }
}
