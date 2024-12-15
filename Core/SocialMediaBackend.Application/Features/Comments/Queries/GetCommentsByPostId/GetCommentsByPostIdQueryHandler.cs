using MediatR;
using Microsoft.EntityFrameworkCore;
using SocialMediaBackend.Application.DTOs.Comments;
using SocialMediaBackend.Application.Repositories.Comments;

namespace SocialMediaBackend.Application.Features.Comments.Queries.GetCommentsByPostId
{
    public class GetCommentsByPostIdQueryHandler : IRequestHandler<GetCommentsByPostIdQueryRequest, GetCommentsByPostIdQueryResponse>
    {
        private readonly ICommentReadRepository _commentReadRepository;

        public GetCommentsByPostIdQueryHandler(ICommentReadRepository commentReadRepository)
        {
            _commentReadRepository = commentReadRepository;
        }

        public async Task<GetCommentsByPostIdQueryResponse> Handle(GetCommentsByPostIdQueryRequest request, CancellationToken cancellationToken)
        {
            var totalCommentCount = _commentReadRepository.GetAll(false)
                .Where(x => x.PostId == Guid.Parse(request.PostId))
                .Where(x => x.ParentCommentId == null)
                .Count();

            var comments = _commentReadRepository.GetAll(false).Where(x => x.PostId == Guid.Parse(request.PostId))
                .Where(x => x.ParentCommentId == null)
                .OrderByDescending(x => x.AppUserId == request.UserId)
                .ThenByDescending(x => x.CreatedDate)
                .Skip(request.Pagination.Page * request.Pagination.Size)
                .Take(request.Pagination.Size)
                .Select(c => new CommentDto
                {
                    Id = c.Id.ToString(),
                    Content = c.Content,
                    UserId = c.AppUserId,
                    UserName = c.AppUser.UserName,
                    UserProfilePhoto = c.AppUser.ProfilePhoto,
                    TotalRepliesCount = c.Replies.Count(),
                    CreatedDate = c.CreatedDate,
                    UpdatedDate = c.UpdatedDate
                });

            return new GetCommentsByPostIdQueryResponse { TotalCommentCount = totalCommentCount, Comments = comments };
        }
    }
}
