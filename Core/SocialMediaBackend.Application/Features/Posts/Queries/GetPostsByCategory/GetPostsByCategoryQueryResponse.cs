using SocialMediaBackend.Application.DTOs.Posts;

namespace SocialMediaBackend.Application.Features.Posts.Queries.GetPostsByCategory
{
    public class GetPostsByCategoryQueryResponse
    {
        public int TotalPostCount { get; set; }
        public IQueryable<PostListDto> Posts { get; set; }
        
    }
}
