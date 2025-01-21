using MediatR;
using Microsoft.Extensions.Configuration;
using SocialMediaBackend.Application.DTOs.PostImages;
using SocialMediaBackend.Application.DTOs.Posts;
using SocialMediaBackend.Application.Repositories.Posts;

namespace SocialMediaBackend.Application.Features.Posts.Queries.GetPublicPosts;

public class GetPublicPostsQueryHandler : IRequestHandler<GetPublicPostsQueryRequest, GetPublicPostsQueryResponse>
{
    private readonly IPostReadRepository _postReadRepository;
    private readonly IConfiguration _configuration;     

    public GetPublicPostsQueryHandler(IPostReadRepository postReadRepository, IConfiguration configuration)
    {
        _postReadRepository = postReadRepository;
        _configuration = configuration;
    }

    public async Task<GetPublicPostsQueryResponse> Handle(GetPublicPostsQueryRequest request, CancellationToken cancellationToken)
    {
        var posts = _postReadRepository.GetAll(false)
            .OrderByDescending(p => p.CreatedDate)
            .Take(request.Limit)
            .Select(p => new PostListDto {
                Id = p.Id,
                Title = p.Title,
                Content = p.Content,
                CategoryId = p.CategoryId,
                CategoryName = p.Category.Title,
                UserId = p.AppUserId,
                UserName = p.AppUser.UserName,
                UserProfilePhoto = p.AppUser.ProfilePhoto,
                IsLiked = false,
                LikeCount = p.LikeCount,
                CommentCount = p.CommentCount,
                CreatedDate = p.CreatedDate,
                UpdatedDate = p.UpdatedDate,
                PostImages = p.PostImages.Select(pi => new PostImagesDto { Path = _configuration["StorageUrls:LocalStorage"] + pi.Path }).ToList()
            });

        return new GetPublicPostsQueryResponse {
            Posts = posts
        };
    }
}
