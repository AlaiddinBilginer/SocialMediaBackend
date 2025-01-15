using MediatR;
using SocialMediaBackend.Application.RequestParameters;

namespace SocialMediaBackend.Application.Features.Posts.Queries.GetPopularPosts;

public class GetPopularPostsQueryRequest : IRequest<GetPopularPostsQueryResponse>
{
    public Pagination Pagination { get; set; } = new Pagination();
    public string? CurrentUserId { get; set; }
}

