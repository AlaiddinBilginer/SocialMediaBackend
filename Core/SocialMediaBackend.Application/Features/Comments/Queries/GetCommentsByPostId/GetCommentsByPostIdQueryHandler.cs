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
            var totalCommentCount = _commentReadRepository.GetAll(false).Where(x => x.PostId == Guid.Parse(request.PostId)).Count();

            var comments = _commentReadRepository.GetAll(false).Where(x => x.PostId == Guid.Parse(request.PostId))
                .Skip(request.Pagination.Page * request.Pagination.Page)
                .Take(request.Pagination.Size)
                .Select(c => new CommentDto
                {
                    Id = nameof(c.Id),
                    Content = c.Content,
                    UserName = c.AppUser.UserName,
                    UserProfilePhoto = c.AppUser.ProfilePhoto,
                    CreatedDate = c.CreatedDate,
                    UpdatedDate = c.UpdatedDate
                });

            return new GetCommentsByPostIdQueryResponse { TotalCommentCount = totalCommentCount, Comments = comments };
        }
    }
}
