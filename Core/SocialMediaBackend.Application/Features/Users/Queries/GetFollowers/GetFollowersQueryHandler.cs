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
        var baseQuery = _followersReadRepository
            .GetWhere(x => x.FollowedUser.UserName == request.UserName)
            .Include(x => x.FollowerUser)
            .AsQueryable();

        if (!string.IsNullOrWhiteSpace(request.SearchTerm))
        {
            string searchTerm = request.SearchTerm.ToLower();
            baseQuery = baseQuery.Where(x =>
                x.FollowerUser.UserName.ToLower().Contains(searchTerm) ||
                x.FollowerUser.FullName.ToLower().Contains(searchTerm));
        }

        var followersCount = await baseQuery.CountAsync(cancellationToken);

        var followers = await baseQuery
            .Skip(request.Pagination.Page * request.Pagination.Size)
            .Take(request.Pagination.Size)
            .ToListAsync(cancellationToken);

        var followedUserIds = followers.Select(x => x.FollowerUserId).ToList();
        
        var followingUserIds = await _followersReadRepository
            .GetWhere(f => f.FollowerUser.UserName == (request.InstantUser ?? request.UserName) 
                        && followedUserIds.Contains(f.FollowedUserId))
            .Select(f => f.FollowedUserId)
            .ToListAsync(cancellationToken);

        var followingUserSet = new HashSet<string>(followingUserIds);

        var followersDto = followers.Select(x => new FollowerUserDto
        {
            Id = x.FollowerUserId,
            UserName = x.FollowerUser.UserName,
            FullName = x.FollowerUser.FullName,
            ProfilePhoto = x.FollowerUser.ProfilePhoto,
            IsFollowing = followingUserSet.Contains(x.FollowerUserId)
        }).ToList();

        return new GetFollowersQueryResponse
        {
            FollowersCount = followersCount,
            Followers = followersDto
        };
    }
}
