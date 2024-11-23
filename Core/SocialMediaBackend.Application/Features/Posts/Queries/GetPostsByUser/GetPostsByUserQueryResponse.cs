using SocialMediaBackend.Application.DTOs.Posts;

namespace SocialMediaBackend.Application.Features.Posts.Queries.GetPostsByUser
{
    public class GetPostsByUserQueryResponse
    {
        public int TotalPostCount { get; set; }
        public IQueryable<PostListDto> Posts { get; set; }
    }
}
