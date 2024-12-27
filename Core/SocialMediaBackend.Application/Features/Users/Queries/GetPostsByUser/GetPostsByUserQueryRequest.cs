using MediatR;
using SocialMediaBackend.Application.RequestParameters;

namespace SocialMediaBackend.Application.Features.Users.Queries.GetPostsByUser
{
    public class GetPostsByUserQueryRequest : IRequest<GetPostsByUserQueryResponse>
    {
        public string UserName { get; set; }
        public Pagination? Pagination { get; set; } = new Pagination();
    }
}
