using MediatR;
using Microsoft.AspNetCore.Identity;
using SocialMediaBackend.Domain.Entities.Identity;
using SocialMediaBackend.Domain.Exceptions;

namespace SocialMediaBackend.Application.Features.Users.Queries.GetUserProfile
{
    public class GetUserProfileQueryHandler : IRequestHandler<GetUserProfileQueryRequest, GetUserProfileQueryResponse>
    {
        private readonly UserManager<AppUser> _userManager;

        public GetUserProfileQueryHandler(UserManager<AppUser> userManager)
        {
            _userManager = userManager;
        }

        public async Task<GetUserProfileQueryResponse> Handle(GetUserProfileQueryRequest request, CancellationToken cancellationToken)
        {
            AppUser? user = await _userManager.FindByNameAsync(request.UserName);

            if (user == null)
                throw new UserNotFoundException();

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
                AccountCreatedDate = user.AccountCreatedDate,
            };
        }
    }
}
