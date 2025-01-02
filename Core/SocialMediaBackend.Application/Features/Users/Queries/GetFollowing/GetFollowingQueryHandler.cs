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
        var baseQuery = _followersReadRepository
            .GetWhere(x => x.FollowerUser.UserName == request.UserName)
            .Include(x => x.FollowedUser)
            .AsQueryable();

        if (!string.IsNullOrWhiteSpace(request.SearchTerm))
        {
            string searchTerm = request.SearchTerm.ToLower();
            baseQuery = baseQuery.Where(x =>
                x.FollowedUser.UserName.ToLower().Contains(searchTerm) ||
                x.FollowedUser.FullName.ToLower().Contains(searchTerm));
        }

        var followingCount = await baseQuery.CountAsync(cancellationToken);

        var followings = await baseQuery
            .Skip(request.Pagination.Page * request.Pagination.Size)
            .Take(request.Pagination.Size)
            .ToListAsync(cancellationToken);

        var followedUserIds = followings.Select(x => x.FollowedUserId).ToList();
        
        var followingUserIds = await _followersReadRepository
            .GetWhere(f => f.FollowerUser.UserName == (request.InstantUser ?? request.UserName) 
                        && followedUserIds.Contains(f.FollowedUserId))
            .Select(f => f.FollowedUserId)
            .ToListAsync(cancellationToken);

        var followingUserSet = new HashSet<string>(followingUserIds);

        var followingsDto = followings.Select(x => new FollowingUserDto
        {
            Id = x.FollowedUser.Id,
            UserName = x.FollowedUser.UserName,
            FullName = x.FollowedUser.FullName,
            ProfilePhoto = x.FollowedUser.ProfilePhoto,
            IsFollowing = followingUserSet.Contains(x.FollowedUserId)
        }).ToList();

        return new GetFollowingQueryResponse
        {
            FollowingCount = followingCount,
            Followings = followingsDto
        };
    }
}
