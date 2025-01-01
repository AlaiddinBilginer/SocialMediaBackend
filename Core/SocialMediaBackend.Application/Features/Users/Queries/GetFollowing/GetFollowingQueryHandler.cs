using MediatR;
using Microsoft.EntityFrameworkCore;
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
        var followingsQuery = _followersReadRepository
            .GetWhere(x => x.FollowerUser.UserName == request.UserName)
            .Include(x => x.FollowedUser)
            .Skip(request.Pagination.Page * request.Pagination.Size)
            .Take(request.Pagination.Size);

        var followings = await followingsQuery.ToListAsync(cancellationToken);

        var followingCount = await _followersReadRepository
            .GetWhere(x => x.FollowerUser.UserName == request.UserName)
            .CountAsync(cancellationToken);

        var followingsDto = followings.Select(x => new FollowingUserDto
        {
            Id = x.FollowedUser.Id,
            UserName = x.FollowedUser.UserName,
            FullName = x.FollowedUser.FullName,
            ProfilePhoto = x.FollowedUser.ProfilePhoto,
            IsFollowing = IsFollowing(request.InstantUser ?? request.UserName, x.FollowedUserId)
        }).ToList();

        return new GetFollowingQueryResponse
        {
            FollowingCount = followingCount,
            Followings = followingsDto
        };
    }

    private bool IsFollowing(string followerUserName, string followedUserId)
    {
        return _followersReadRepository
            .GetWhere(f => f.FollowerUser.UserName == followerUserName && f.FollowedUserId == followedUserId, false)
            .Any();
    }
}
