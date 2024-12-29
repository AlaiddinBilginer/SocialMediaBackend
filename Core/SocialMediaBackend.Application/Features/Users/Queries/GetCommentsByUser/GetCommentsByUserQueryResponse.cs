using SocialMediaBackend.Application.DTOs.Comments;

namespace SocialMediaBackend.Application.Features.Users.Queries.GetCommentsByUser
{
    public class GetCommentsByUserQueryResponse
    {
        public int TotalCommentsCount { get; set; }
        public IQueryable<GetCommentsByUserDto> Comments { get; set; }
    }
}
