using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using SocialMediaBackend.Application.Repositories.Followers;
using SocialMediaBackend.Domain.Entities;
using SocialMediaBackend.Domain.Entities.Identity;
using SocialMediaBackend.Domain.Exceptions;

namespace SocialMediaBackend.Application.Features.Users.Commands.UnfollowUser
{
    public class UnfollowUserCommandHandler : IRequestHandler<UnfollowUserCommandRequest, UnfollowUserCommandResponse>
    {
        private readonly IFollowersWriteRepository _followersWriteRepository;
        private readonly IFollowersReadRepository _followersReadRepository;
        private readonly IHttpContextAccessor _contextAccessor;
        private readonly UserManager<AppUser> _userManager;

        public UnfollowUserCommandHandler(
            IFollowersWriteRepository followersWriteRepository, IFollowersReadRepository followersReadRepository,
            IHttpContextAccessor contextAccessor, 
            UserManager<AppUser> userManager)
        {
            _followersWriteRepository = followersWriteRepository;
            _followersReadRepository = followersReadRepository;
            _contextAccessor = contextAccessor;
            _userManager = userManager;
        }

        public async Task<UnfollowUserCommandResponse> Handle(UnfollowUserCommandRequest request, CancellationToken cancellationToken)
        {
            string? userName = _contextAccessor?.HttpContext?.User?.Identity?.Name;
            AppUser? followerUser = await _userManager.Users.FirstOrDefaultAsync(x => x.UserName == userName);
            AppUser? followedUser = await _userManager.Users.FirstOrDefaultAsync(x => x.UserName == request.UnfollowedUserName);

            if (followerUser == null || followedUser == null)
                throw new UserNotFoundException();

            Follower follower = await _followersReadRepository.GetSingleAsync(x =>
                x.FollowerUser.UserName == followerUser.UserName && x.FollowedUser.UserName == followedUser.UserName);

            await _followersWriteRepository.DeleteByIdAsync(follower.Id.ToString());
            await _followersWriteRepository.SaveAsync();

            followerUser.FollowingCount--;
            followedUser.FollowersCount--;
            await _userManager.UpdateAsync(followerUser);
            await _userManager.UpdateAsync(followedUser);

            return new UnfollowUserCommandResponse { Succeeded = true, Message = $"Artık {followedUser.FullName} kullanıcısını takip etmiyorsunuz" };
        }
    }
}
