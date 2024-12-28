using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using SocialMediaBackend.Application.Repositories.Followers;
using SocialMediaBackend.Domain.Entities;
using SocialMediaBackend.Domain.Entities.Identity;
using SocialMediaBackend.Domain.Exceptions;

namespace SocialMediaBackend.Application.Features.Users.Queries.GetUserProfile
{
    public class GetUserProfileQueryHandler : IRequestHandler<GetUserProfileQueryRequest, GetUserProfileQueryResponse>
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly IHttpContextAccessor _contextAccessor;
        private readonly IFollowersReadRepository _followersReadRepository;

        public GetUserProfileQueryHandler(UserManager<AppUser> userManager, IHttpContextAccessor contextAccessor, IFollowersReadRepository followersReadRepository)
        {
            _userManager = userManager;
            _contextAccessor = contextAccessor;
            _followersReadRepository = followersReadRepository;
        }

        public async Task<GetUserProfileQueryResponse> Handle(GetUserProfileQueryRequest request, CancellationToken cancellationToken)
        {
            string? userName = _contextAccessor?.HttpContext?.User?.Identity?.Name;
            AppUser? user = await _userManager.FindByNameAsync(request.UserName);

            if (user == null)
                throw new UserNotFoundException();

            bool isFollower = _followersReadRepository.GetWhere(x =>
                x.FollowerUser.UserName == userName && x.FollowedUser.UserName == request.UserName).Any();

            return new GetUserProfileQueryResponse
            {
                UserName = request.UserName,
                FullName = user.FullName,
                Bio = user.Bio,
                CoverPhoto = user.CoverPhoto,
                ProfilePhoto = user.ProfilePhoto,
                PostsCount = user.PostsCount,
                CommentsCount = user.CommentsCount,
                FollowersCount = user.FollowersCount,
                FollowingCount = user.FollowingCount,
                IsFollower = isFollower,
                AccountCreatedDate = user.AccountCreatedDate,

            };
        }
    }
}
