using MediatR;
using Microsoft.EntityFrameworkCore;
using SocialMediaBackend.Application.Repositories.CommentLikes;
using SocialMediaBackend.Application.Repositories.Comments;
using SocialMediaBackend.Domain.Entities;

namespace SocialMediaBackend.Application.Features.Comments.Commands.LikeComment;

public class LikeCommentCommandHandler : IRequestHandler<LikeCommentCommandRequest, LikeCommentCommandResponse>
{
    private readonly ICommentLikeWriteRepository _commentLikeWriteRepository;
    private readonly ICommentLikeReadRepository _commentLikeReadRepository;
    private readonly ICommentReadRepository _commentReadRepository;
    private readonly ICommentWriteRepository _commentWriteRepository;

    public LikeCommentCommandHandler(
        ICommentLikeWriteRepository commentLikeWriteRepository, 
        ICommentLikeReadRepository commentLikeReadRepository,
        ICommentReadRepository commentReadRepository,
        ICommentWriteRepository commentWriteRepository)
    {
        _commentLikeWriteRepository = commentLikeWriteRepository;
        _commentLikeReadRepository = commentLikeReadRepository;
        _commentReadRepository = commentReadRepository;
        _commentWriteRepository = commentWriteRepository;
    }

    public async Task<LikeCommentCommandResponse> Handle(LikeCommentCommandRequest request, CancellationToken cancellationToken)
    {
        var commentLiked = await _commentLikeReadRepository.GetWhere(x => x.CommentId == Guid.Parse(request.CommentId) && x.UserId == request.UserId, false).AnyAsync();
        PostComment comment = await _commentReadRepository.GetByIdAsync(request.CommentId);

        if (commentLiked) {
            await _commentLikeWriteRepository.DeleteWhere(x => x.CommentId == Guid.Parse(request.CommentId) && x.UserId == request.UserId);
            comment.LikeCount--;
            await _commentWriteRepository.SaveAsync();
            return new LikeCommentCommandResponse { Succeeded = true, Message = "Yorum beğenisi çekildi." };
        }

        await _commentLikeWriteRepository.AddAsync(new CommentLike { CommentId = Guid.Parse(request.CommentId), UserId = request.UserId });
        await _commentLikeWriteRepository.SaveAsync();

        comment.LikeCount++;
        await _commentWriteRepository.SaveAsync();

        return new LikeCommentCommandResponse { Succeeded = true, Message = "Yorum beğenildi." };
    }
}
