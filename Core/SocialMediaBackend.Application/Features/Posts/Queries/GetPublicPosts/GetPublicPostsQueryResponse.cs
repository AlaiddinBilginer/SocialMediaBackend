using SocialMediaBackend.Application.DTOs.Posts;

namespace SocialMediaBackend.Application.Features.Posts.Queries.GetPublicPosts;

public class GetPublicPostsQueryResponse
{
    public IQueryable<PostListDto> Posts { get; set; }
}
