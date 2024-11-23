using MediatR;
using SocialMediaBackend.Application.RequestParameters;

namespace SocialMediaBackend.Application.Features.Posts.Queries.GetPostsByUser
{
    public class GetPostsByUserQueryRequest : IRequest<GetPostsByUserQueryResponse>
    {
        public Pagination? Pagination { get; set; } = new Pagination();
    }
}
