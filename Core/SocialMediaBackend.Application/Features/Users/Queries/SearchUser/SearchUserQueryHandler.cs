using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using SocialMediaBackend.Application.DTOs.Users;
using SocialMediaBackend.Domain.Entities.Identity;

namespace SocialMediaBackend.Application.Features.Users.Queries.SearchUser;

public class SearchUserQueryHandler : IRequestHandler<SearchUserQueryRequest, SearchUserQueryResponse>
{
    private readonly UserManager<AppUser> _userManager;

    public SearchUserQueryHandler(UserManager<AppUser> userManager)
    {
        _userManager = userManager;
    }

    public async Task<SearchUserQueryResponse> Handle(SearchUserQueryRequest request, CancellationToken cancellationToken)
    {
        string searchTerm = request.SearchTerm?.ToLower() ?? string.Empty;

        var usersQuery = _userManager.Users.AsQueryable();

        if (!string.IsNullOrEmpty(searchTerm))
        {
            usersQuery = usersQuery.Where(u => u.UserName.ToLower().Contains(searchTerm) || u.FullName.ToLower().Contains(searchTerm));
        }

        var userCount = await usersQuery.CountAsync(cancellationToken);

        var users = await usersQuery
            .Skip(request.Pagination.Size * request.Pagination.Page)
            .Take(request.Pagination.Size)
            .Select(u => new SearchUserDto
            {
                Id = u.Id,
                UserName = u.UserName,
                FullName = u.FullName,
                ProfilePhoto = u.ProfilePhoto,
            })
            .ToListAsync(cancellationToken);

        return new SearchUserQueryResponse
        {
            UserCount = userCount,
            Users = users,
        };
    }
}
