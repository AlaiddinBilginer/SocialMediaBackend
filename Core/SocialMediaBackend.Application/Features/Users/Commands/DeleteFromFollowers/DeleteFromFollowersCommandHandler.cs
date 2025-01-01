using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using SocialMediaBackend.Application.Repositories.Followers;
using SocialMediaBackend.Domain.Entities;
using SocialMediaBackend.Domain.Entities.Identity;
using SocialMediaBackend.Domain.Exceptions;

namespace SocialMediaBackend.Application.Features.Users.Commands.DeleteFromFollowers;

public class DeleteFromFollowersCommandHandler : IRequestHandler<DeleteFromFollowersCommandRequest, DeleteFromFollowersCommandResponse>
{
    private readonly IFollowersWriteRepository _followersWriteRepository;
    private readonly IFollowersReadRepository _followersReadRepository;
    private readonly IHttpContextAccessor _httpContextAccessor;
    private readonly UserManager<AppUser> _userManager;

    public DeleteFromFollowersCommandHandler(IFollowersWriteRepository followersWriteRepository, IFollowersReadRepository followersReadRepository, IHttpContextAccessor httpContextAccessor, UserManager<AppUser> userManager)
    {
        _followersWriteRepository = followersWriteRepository;
        _followersReadRepository = followersReadRepository;
        _httpContextAccessor = httpContextAccessor;
        _userManager = userManager;
    }
    public async Task<DeleteFromFollowersCommandResponse> Handle(DeleteFromFollowersCommandRequest request, CancellationToken cancellationToken)
    {
        string? userName = _httpContextAccessor.HttpContext?.User.Identity?.Name;
        AppUser? user = await _userManager.FindByNameAsync(userName);
        AppUser? userToDelete = await _userManager.FindByIdAsync(request.UserId);

        if (user == null || userToDelete == null)
            throw new UserNotFoundException();

        Follower follower = await _followersReadRepository.GetSingleAsync(f => f.FollowedUserId == user.Id && f.FollowerUserId == request.UserId);

        _followersWriteRepository.Delete(follower);
        await _followersWriteRepository.SaveAsync();

        user.FollowersCount--;
        await _userManager.UpdateAsync(user);
        userToDelete.FollowingCount--;
        await _userManager.UpdateAsync(userToDelete);

        return new DeleteFromFollowersCommandResponse { Succeeded = true, Message = "Takipçi başarıyla silindi" };
    }
}
