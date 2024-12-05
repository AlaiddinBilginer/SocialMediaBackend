using MediatR;
using SocialMediaBackend.Application.RequestParameters;

namespace SocialMediaBackend.Application.Features.Comments.Queries.GetCommentsByPostId
{
    public class GetCommentsByPostIdQueryRequest : IRequest<GetCommentsByPostIdQueryResponse>
    {
        public string PostId { get; set; }
        public Pagination? Pagination { get; set; } = new Pagination();
    }
}
