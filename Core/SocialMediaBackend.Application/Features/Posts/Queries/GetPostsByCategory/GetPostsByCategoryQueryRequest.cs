using MediatR;
using SocialMediaBackend.Application.RequestParameters;

namespace SocialMediaBackend.Application.Features.Posts.Queries.GetPostsByCategory
{
    public class GetPostsByCategoryQueryRequest : IRequest<GetPostsByCategoryQueryResponse>
    {
        public Pagination? Pagination { get; set; } = new Pagination();
        public string CategoryName { get; set; }
    }
}
