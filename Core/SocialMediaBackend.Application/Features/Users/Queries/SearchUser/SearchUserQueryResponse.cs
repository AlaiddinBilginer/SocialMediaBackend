using SocialMediaBackend.Application.DTOs.Users;

namespace SocialMediaBackend.Application.Features.Users.Queries.SearchUser;

public class SearchUserQueryResponse
{
    public int UserCount { get; set; }
    public List<SearchUserDto> Users { get; set; }
}
