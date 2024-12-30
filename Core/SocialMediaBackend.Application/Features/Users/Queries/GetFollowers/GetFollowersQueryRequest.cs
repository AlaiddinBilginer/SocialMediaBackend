using MediatR;
using SocialMediaBackend.Application.RequestParameters;

namespace SocialMediaBackend.Application.Features.Users.Queries.GetFollowers;

public class GetFollowersQueryRequest : IRequest<GetFollowersQueryResponse>
{
    public string UserName { get; set; }
    public Pagination Pagination { get; set; } = new Pagination();
}