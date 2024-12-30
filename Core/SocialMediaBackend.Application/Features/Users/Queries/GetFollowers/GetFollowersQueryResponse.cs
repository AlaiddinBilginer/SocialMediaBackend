using SocialMediaBackend.Application.DTOs.Users;
using System.Collections.Generic;

namespace SocialMediaBackend.Application.Features.Users.Queries.GetFollowers;

public class GetFollowersQueryResponse
{
    public int FollowersCount { get; set; }
    public IEnumerable<FollowerUserDto> Followers { get; set; }
}
