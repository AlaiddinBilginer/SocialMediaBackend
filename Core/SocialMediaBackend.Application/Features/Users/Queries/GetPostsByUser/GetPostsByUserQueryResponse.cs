using SocialMediaBackend.Application.DTOs.Posts;

namespace SocialMediaBackend.Application.Features.Users.Queries.GetPostsByUser
{
    public class GetPostsByUserQueryResponse
    {
        public int TotalPostCount { get; set; }
        public IQueryable<PostListDto> Posts { get; set; }
    }
}
