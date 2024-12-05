using SocialMediaBackend.Application.DTOs.Comments;

namespace SocialMediaBackend.Application.Features.Comments.Queries.GetCommentsByPostId
{
    public class GetCommentsByPostIdQueryResponse
    {
        public int TotalCommentCount { get; set; }
        public IQueryable<CommentDto> Comments { get; set; }
    }
}
