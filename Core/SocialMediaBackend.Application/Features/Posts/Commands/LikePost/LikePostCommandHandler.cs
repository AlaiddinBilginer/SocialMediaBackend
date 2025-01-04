using System.Security.Claims;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using SocialMediaBackend.Application.Repositories.PostLikes;
using SocialMediaBackend.Application.Repositories.Posts;
using SocialMediaBackend.Domain.Entities;

namespace SocialMediaBackend.Application.Features.Posts.Commands.LikePost;

public class LikePostCommandHandler : IRequestHandler<LikePostCommandRequest, LikePostCommandResponse>
{
    private readonly IPostLikeWriteRepository _postLikeWriteRepository;
    private readonly IPostLikeReadRepository _postLikeReadRepository;
    private readonly IPostReadRepository _postReadRepository;
    private readonly IPostWriteRepository _postWriteRepository;

    public LikePostCommandHandler(
        IPostLikeWriteRepository postLikeWriteRepository, 
        IPostLikeReadRepository postLikeReadRepository, 
        IPostReadRepository postReadRepository,
        IPostWriteRepository postWriteRepository)
    {
        _postLikeWriteRepository = postLikeWriteRepository;
        _postLikeReadRepository = postLikeReadRepository;
        _postReadRepository = postReadRepository;
        _postWriteRepository = postWriteRepository;
    }
    public async Task<LikePostCommandResponse> Handle(LikePostCommandRequest request, CancellationToken cancellationToken)
    {
        var postLiked =  await _postLikeReadRepository.GetWhere(x => x.PostId == Guid.Parse(request.PostId) && x.AppUserId == request.UserId, false).AnyAsync();
        Post post = await _postReadRepository.GetByIdAsync(request.PostId);

        if (postLiked) {
            await _postLikeWriteRepository.DeleteWhere(x => x.PostId == Guid.Parse(request.PostId) && x.AppUserId == request.UserId);
            post.LikeCount--;
            await _postWriteRepository.SaveAsync();
            return new LikePostCommandResponse { Succeeded = true, Message = "Gönderiden beğeni çekildi." };
        }
            
        await _postLikeWriteRepository.AddAsync(new PostLike { AppUserId = request.UserId, PostId = Guid.Parse(request.PostId ) });
        await _postLikeWriteRepository.SaveAsync();

        post.LikeCount++;
        await _postWriteRepository.SaveAsync();

        return new LikePostCommandResponse { Succeeded = true, Message = "Gönderi başarıyla beğenildi." };
    }
}
