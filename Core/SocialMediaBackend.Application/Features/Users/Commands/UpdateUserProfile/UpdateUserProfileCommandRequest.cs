using MediatR;

namespace SocialMediaBackend.Application.Features.Users.Commands.UpdateUserProfile
{
    public class UpdateUserProfileCommandRequest : IRequest<UpdateUserProfileCommandResponse>
    {
        public string UserName { get; set; }
        public string FullName { get; set; }
        public string? Bio { get; set; }
    }
}
