using MediatR;
using SocialMediaBackend.Application.Repositories.Followers;
using SocialMediaBackend.Application.DTOs.Users;
using Microsoft.EntityFrameworkCore;

namespace SocialMediaBackend.Application.Features.Users.Queries.GetFollowers;

public class GetFollowersQueryHandler : IRequestHandler<GetFollowersQueryRequest, GetFollowersQueryResponse>
{
    private readonly IFollowersReadRepository _followersReadRepository;

    public GetFollowersQueryHandler(IFollowersReadRepository followersReadRepository)
    {
        _followersReadRepository = followersReadRepository;
    }

    public async Task<GetFollowersQueryResponse> Handle(GetFollowersQueryRequest request, CancellationToken cancellationToken)
    {   
        var followersQuery = _followersReadRepository
            .GetWhere(x => x.FollowedUser.UserName == request.UserName)
            .Include(x => x.FollowerUser)
            .Skip(request.Pagination.Page * request.Pagination.Size)
            .Take(request.Pagination.Size);

        var followers = await followersQuery.ToListAsync(cancellationToken);

        var followersCount = await _followersReadRepository
            .GetWhere(x => x.FollowedUser.UserName == request.UserName)
            .CountAsync(cancellationToken);

        var followersDto = followers.Select(x => new FollowerUserDto
        {
            Id = x.FollowerUserId,
            UserName = x.FollowerUser.UserName,
            FullName = x.FollowerUser.FullName,
            ProfilePhoto = x.FollowerUser.ProfilePhoto,
            IsFollowing = _followersReadRepository
                .GetWhere(f => f.FollowerUser.UserName == request.UserName && f.FollowedUserId == x.FollowerUserId, false)
                .Any()
        }).ToList();

        return new GetFollowersQueryResponse
        {
            FollowersCount = followersCount,
            Followers = followersDto
        };
    }
}
