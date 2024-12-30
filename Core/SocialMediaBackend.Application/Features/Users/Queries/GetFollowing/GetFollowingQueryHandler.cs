using MediatR;
using SocialMediaBackend.Application.DTOs.Users;
using SocialMediaBackend.Application.Repositories.Followers;

namespace SocialMediaBackend.Application.Features.Users.Queries.GetFollowing;

public class GetFollowingQueryHandler : IRequestHandler<GetFollowingQueryRequest, GetFollowingQueryResponse>
{
    private readonly IFollowersReadRepository _followersReadRepository;

    public GetFollowingQueryHandler(IFollowersReadRepository followersReadRepository)
    {
        _followersReadRepository = followersReadRepository;
    }

    public async Task<GetFollowingQueryResponse> Handle(GetFollowingQueryRequest request, CancellationToken cancellationToken)
    {
        var followings = _followersReadRepository
            .GetWhere(x => x.FollowerUser.UserName == request.UserName)
            .Skip(request.Pagination.Page * request.Pagination.Size)
            .Take(request.Pagination.Size);
        var followingsCount = followings.Count();
        var followingsDto = followings.Select(x => new FollowingUserDto
        {
            Id = x.FollowedUser.Id,
            UserName = x.FollowedUser.UserName,
            FullName = x.FollowedUser.FullName,
            ProfilePhoto = x.FollowedUser.ProfilePhoto
        });

        return new GetFollowingQueryResponse
        {
            FollowingCount = followingsCount,
            Followings = followingsDto
        };
    }
}
