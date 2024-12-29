using MediatR;
using SocialMediaBackend.Application.RequestParameters;

namespace SocialMediaBackend.Application.Features.Users.Queries.GetCommentsByUser
{
    public class GetCommentsByUserQueryRequest : IRequest<GetCommentsByUserQueryResponse>
    {
        public Pagination Pagination { get; set; } = new Pagination();
        public string UserName { get; set; }
    }
}
