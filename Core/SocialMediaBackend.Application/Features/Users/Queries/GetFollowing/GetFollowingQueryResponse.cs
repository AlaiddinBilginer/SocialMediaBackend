using SocialMediaBackend.Application.DTOs.Users;

namespace SocialMediaBackend.Application.Features.Users.Queries.GetFollowing;

public class GetFollowingQueryResponse
{
    public int FollowingCount { get; set; }
    public IQueryable<FollowingUserDto> Followings { get; set; }
}
