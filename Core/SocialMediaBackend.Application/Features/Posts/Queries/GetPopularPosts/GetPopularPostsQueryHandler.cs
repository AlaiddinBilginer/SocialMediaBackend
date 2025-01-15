using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using SocialMediaBackend.Application.Common.Interfaces;
using SocialMediaBackend.Application.DTOs.PostImages;
using SocialMediaBackend.Application.DTOs.Posts;
using SocialMediaBackend.Application.Repositories.Posts;

namespace SocialMediaBackend.Application.Features.Posts.Queries.GetPopularPosts;

public class GetPopularPostsQueryHandler : IRequestHandler<GetPopularPostsQueryRequest, GetPopularPostsQueryResponse>
{
    private readonly IPostReadRepository _postReadRepository;
    private readonly IConfiguration _configuration;

    public GetPopularPostsQueryHandler(IPostReadRepository postReadRepository, IConfiguration configuration)
    {
        _postReadRepository = postReadRepository;
        _configuration = configuration;
    }

    public async Task<GetPopularPostsQueryResponse> Handle(GetPopularPostsQueryRequest request, CancellationToken cancellationToken)
    {
        var totalPostCount = await _postReadRepository.GetAll(false).CountAsync(cancellationToken);

        var posts = await _postReadRepository.GetAll(false)
            .OrderByDescending(p => p.LikeCount)
            .Skip(request.Pagination.Size * request.Pagination.Page)
            .Take(request.Pagination.Size)
            .Select(p => new PostListDto
            {
                Id = p.Id,
                Title = p.Title,
                Content = p.Content,
                CategoryId = p.CategoryId,
                CategoryName = p.Category.Title,
                UserId = p.AppUserId,
                UserName = p.AppUser.UserName,
                UserProfilePhoto = p.AppUser.ProfilePhoto,
                IsLiked = p.Likes.Any(x => x.AppUserId == request.CurrentUserId),
                LikeCount = p.LikeCount,
                CreatedDate = p.CreatedDate,
                UpdatedDate = p.UpdatedDate,
                PostImages = p.PostImages.Select(pi => new PostImagesDto
                {
                    Path = _configuration["StorageUrls:LocalStorage"] + pi.Path
                }).ToList()
            })
            .ToListAsync(cancellationToken);

        return new GetPopularPostsQueryResponse
        {
            TotalPostCount = totalPostCount,
            Posts = posts,
        };
    }
}


