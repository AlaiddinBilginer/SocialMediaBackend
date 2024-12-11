using MediatR;

namespace SocialMediaBackend.Application.Features.Users.Queries.GetUserProfile
{
    public class GetUserProfileQueryRequest : IRequest<GetUserProfileQueryResponse>
    {
        public string UserName { get; set; }
    }
}
