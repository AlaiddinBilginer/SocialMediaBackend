using MediatR;
using SocialMediaBackend.Application.DTOs.Comments;
using SocialMediaBackend.Application.Repositories.Comments;

namespace SocialMediaBackend.Application.Features.Comments.Queries.GetRepliesByParentCommentId
{
    public class GetRepliesByParentCommentIdQueryHandler : IRequestHandler<GetRepliesByParentCommentIdQueryRequest, GetRepliesByParentCommentIdQueryResponse>
    {
        private readonly ICommentReadRepository _commentReadRepository;

        public GetRepliesByParentCommentIdQueryHandler(ICommentReadRepository commentReadRepository)
        {
            _commentReadRepository = commentReadRepository;
        }

        public async Task<GetRepliesByParentCommentIdQueryResponse> Handle(GetRepliesByParentCommentIdQueryRequest request, CancellationToken cancellationToken)
        {

            var replies = _commentReadRepository.GetWhere(x => x.ParentCommentId == Guid.Parse(request.ParentCommentId))
                .OrderBy(x => x.CreatedDate)
                .Skip(request.Pagination.Page * request.Pagination.Size)
                .Take(request.Pagination.Size)
                .Select(r => new ReplyCommentDto
                {
                    Id = r.Id.ToString(),
                    UserId = r.AppUserId,
                    Content = r.Content,
                    UserName = r.AppUser.UserName,
                    UserProfilePhoto = r.AppUser.ProfilePhoto,
                    CreatedDate = r.CreatedDate,
                    UpdatedDate = r.UpdatedDate,
                });

            return new GetRepliesByParentCommentIdQueryResponse { Replies = replies };
        }
    }
}
