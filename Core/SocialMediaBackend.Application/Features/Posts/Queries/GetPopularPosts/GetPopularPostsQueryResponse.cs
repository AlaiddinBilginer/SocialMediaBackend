using SocialMediaBackend.Application.DTOs.Posts;

namespace SocialMediaBackend.Application.Features.Posts.Queries.GetPopularPosts;

public class GetPopularPostsQueryResponse
{
    public int TotalPostCount { get; set; }
    public List<PostListDto> Posts { get; set; }
}

