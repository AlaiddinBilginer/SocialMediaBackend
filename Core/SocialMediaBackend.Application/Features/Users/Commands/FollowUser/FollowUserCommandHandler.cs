using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using SocialMediaBackend.Application.Repositories.Followers;
using SocialMediaBackend.Domain.Entities;
using SocialMediaBackend.Domain.Entities.Identity;
using SocialMediaBackend.Domain.Exceptions;

namespace SocialMediaBackend.Application.Features.Users.Commands.FollowUser
{
    public class FollowUserCommandHandler : IRequestHandler<FollowUserCommandRequest, FollowUserCommandResponse>
    {
        private readonly IFollowersWriteRepository _followersWriteRepository;
        private readonly UserManager<AppUser> _userManager;
        private readonly IHttpContextAccessor _contextAccessor;

        public FollowUserCommandHandler(IFollowersWriteRepository followersWriteRepository, UserManager<AppUser> userManager, IHttpContextAccessor contextAccessor)
        {
            _followersWriteRepository = followersWriteRepository;
            _userManager = userManager;
            _contextAccessor = contextAccessor;
        }

        public async Task<FollowUserCommandResponse> Handle(FollowUserCommandRequest request, CancellationToken cancellationToken)
        {
            string? userName = _contextAccessor?.HttpContext?.User?.Identity?.Name;
            AppUser? followerUser = await _userManager.Users.FirstOrDefaultAsync(x => x.UserName == userName);
            AppUser? followedUser = await _userManager.Users.FirstOrDefaultAsync(x => x.UserName == request.FollowedUserName);

            if (followerUser == null || followedUser == null)
                throw new UserNotFoundException();

            Follower follower = new Follower
            {
                FollowerUserId = followerUser.Id,
                FollowedUserId = followedUser.Id,
            };

            await _followersWriteRepository.AddAsync(follower);
            await _followersWriteRepository.SaveAsync();

            followerUser.FollowingCount++;
            followedUser.FollowersCount++;
            await _userManager.UpdateAsync(followerUser);
            await _userManager.UpdateAsync(followedUser);

            return new FollowUserCommandResponse { Succeeded = true, Message = $"{followedUser.FullName} kullanıcısı takip edildi." };
        }
    }
}
