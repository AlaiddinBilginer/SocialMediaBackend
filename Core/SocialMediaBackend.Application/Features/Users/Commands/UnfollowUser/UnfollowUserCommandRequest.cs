using MediatR;

namespace SocialMediaBackend.Application.Features.Users.Commands.UnfollowUser
{
    public class UnfollowUserCommandRequest : IRequest<UnfollowUserCommandResponse>
    {
        public string UnfollowedUserName { get; set; }
    }
}
