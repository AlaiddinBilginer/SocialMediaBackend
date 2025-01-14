using MediatR;

namespace SocialMediaBackend.Application.Features.Posts.Queries.GetPublicPosts;

public class GetPublicPostsQueryRequest : IRequest<GetPublicPostsQueryResponse>
{
    public int Limit { get; set; }
}
