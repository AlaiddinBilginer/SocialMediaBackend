using MediatR;
using Microsoft.EntityFrameworkCore;
using SocialMediaBackend.Application.DTOs.PostImages;
using SocialMediaBackend.Application.DTOs.Tags;
using SocialMediaBackend.Application.Repositories.Posts;

namespace SocialMediaBackend.Application.Features.Posts.Queries.GetByIdPost
{
    public class GetByIdPostQueryHandler : IRequestHandler<GetByIdPostQueryRequest, GetByIdPostQueryResponse>
    {
        private readonly IPostReadRepository _postReadRepository;

        public GetByIdPostQueryHandler(IPostReadRepository postReadRepository)
        {
            _postReadRepository = postReadRepository;
        }

        public async Task<GetByIdPostQueryResponse> Handle(GetByIdPostQueryRequest request, CancellationToken cancellationToken)
        {
            var post = await _postReadRepository.GetWhere(p => p.Id == Guid.Parse(request.Id))
                .Include(p => p.PostImages)
                .Include(p => p.AppUser)
                .Include(p => p.Tags)
                .Include(p => p.Category)
                .Select(p => new GetByIdPostQueryResponse
                {
                    Content = p.Content,
                    Title = p.Title,
                    CategoryId = p.CategoryId.ToString(),
                    CategoryName = p.Category.Title,
                    UserId = p.AppUserId,
                    UserName = p.AppUser.UserName,
                    UserPhoto = p.AppUser.ProfilePhoto,
                    Tags = p.Tags.Select(t => new TagDto { Id = t.Id, Title = t.Title }).ToList(),
                    PostImages = p.PostImages.Select(pi => new PostImagesDto { Path = pi.Path }).ToList()
                }).FirstOrDefaultAsync();

            return post;
        }
    }
}
