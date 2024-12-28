using MediatR;

namespace SocialMediaBackend.Application.Features.Users.Commands.FollowUser
{
    public class FollowUserCommandRequest : IRequest<FollowUserCommandResponse>
    {
        public string FollowedUserName { get; set; }
    }
}
