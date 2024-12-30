using MediatR;
using SocialMediaBackend.Application.RequestParameters;

namespace SocialMediaBackend.Application.Features.Users.Queries.GetFollowing;

public class GetFollowingQueryRequest : IRequest<GetFollowingQueryResponse>
{
    public string UserName { get; set; }
    public Pagination Pagination { get; set; } = new Pagination();      
}
