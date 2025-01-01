using MediatR;

namespace SocialMediaBackend.Application.Features.Users.Commands.DeleteFromFollowers;

public class DeleteFromFollowersCommandRequest : IRequest<DeleteFromFollowersCommandResponse>
{
    public string UserId { get; set; }
}
