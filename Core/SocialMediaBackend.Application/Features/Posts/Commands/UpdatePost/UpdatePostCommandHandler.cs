using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using SocialMediaBackend.Application.Repositories.Posts;
using SocialMediaBackend.Domain.Entities;
using SocialMediaBackend.Domain.Entities.Identity;

namespace SocialMediaBackend.Application.Features.Posts.Commands.UpdatePost
{
    public class UpdatePostCommandHandler : IRequestHandler<UpdatePostCommandRequest, UpdatePostCommandResponse>
    {
        private readonly IPostReadRepository _postReadRepository;
        private readonly IPostWriteRepository _postWriteRepository;
        private readonly IHttpContextAccessor _contextAccessor;
        private readonly UserManager<AppUser> _userManager;

        public UpdatePostCommandHandler(
            IPostReadRepository postReadRepository, 
            IPostWriteRepository postWriteRepository, 
            IHttpContextAccessor contextAccessor, 
            UserManager<AppUser> userManager)
        {
            _postReadRepository = postReadRepository;
            _postWriteRepository = postWriteRepository;
            _contextAccessor = contextAccessor;
            _userManager = userManager;
        }

        public async Task<UpdatePostCommandResponse> Handle(UpdatePostCommandRequest request, CancellationToken cancellationToken)
        {
            string? userName = _contextAccessor?.HttpContext?.User?.Identity?.Name;
            AppUser? user = await _userManager.Users.FirstOrDefaultAsync(u => u.UserName == userName);
            Post post = await _postReadRepository.GetByIdAsync(request.Id);

            if(user == null || user.Id != post.AppUserId)
                return new UpdatePostCommandResponse() { Succeeded = false, Message = "Hata meydana geldi"};


            post.Title = request.Title;
            post.Content = request.Content;
            post.CategoryId = Guid.Parse(request.CategoryId);

            await _postWriteRepository.SaveAsync();

            return new UpdatePostCommandResponse() { Succeeded = true, Message = "Gönderi başarılı bir şekilde güncellendi" };

        }
    }
}
