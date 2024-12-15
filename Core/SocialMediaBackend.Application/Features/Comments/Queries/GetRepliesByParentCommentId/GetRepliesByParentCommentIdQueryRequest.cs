using MediatR;
using SocialMediaBackend.Application.RequestParameters;

namespace SocialMediaBackend.Application.Features.Comments.Queries.GetRepliesByParentCommentId
{
    public class GetRepliesByParentCommentIdQueryRequest : IRequest<GetRepliesByParentCommentIdQueryResponse>
    {
        public string ParentCommentId { get; set; }
        public Pagination? Pagination { get; set; } = new Pagination();
    }
}
