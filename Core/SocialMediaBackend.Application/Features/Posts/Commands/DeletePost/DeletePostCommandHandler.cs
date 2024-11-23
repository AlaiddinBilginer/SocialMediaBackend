using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using SocialMediaBackend.Application.Repositories.Posts;
using SocialMediaBackend.Domain.Entities;
using SocialMediaBackend.Domain.Entities.Identity;

namespace SocialMediaBackend.Application.Features.Posts.Commands.DeletePost
{
    public class DeletePostCommandHandler : IRequestHandler<DeletePostCommandRequest, DeletePostCommandResponse>
    {
        private readonly IPostWriteRepository _postWriteRepository;
        private readonly IPostReadRepository _postReadRepository;
        private readonly IHttpContextAccessor _contextAccessor;
        private readonly UserManager<AppUser> _userManager;

        public DeletePostCommandHandler(
            IPostWriteRepository postWriteRepository, 
            IPostReadRepository postReadRepository, 
            IHttpContextAccessor contextAccessor, 
            UserManager<AppUser> userManager)
        {
            _postWriteRepository = postWriteRepository;
            _postReadRepository = postReadRepository;
            _contextAccessor = contextAccessor;
            _userManager = userManager;
        }

        public async Task<DeletePostCommandResponse> Handle(DeletePostCommandRequest request, CancellationToken cancellationToken)
        {
            string? userName = _contextAccessor?.HttpContext?.User?.Identity?.Name;
            AppUser? user = await _userManager.Users.FirstOrDefaultAsync(u => u.UserName == userName);
            Post post = await _postReadRepository.GetByIdAsync(request.Id);

            if(user == null || post.AppUserId != user?.Id) 
                return new DeletePostCommandResponse() { Succeeded = false, Message = "Hata meydana geldi" };

            await _postWriteRepository.DeleteByIdAsync(request.Id);
            await _postWriteRepository.SaveAsync();

            return new DeletePostCommandResponse() { Succeeded = true, Message = "Gönderi başarılı bir şekilde silinmiştir" };
        }
    }
}
