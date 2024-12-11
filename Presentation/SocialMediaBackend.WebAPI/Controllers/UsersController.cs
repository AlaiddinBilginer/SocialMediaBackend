using MediatR;
using Microsoft.AspNetCore.Mvc;
using SocialMediaBackend.Application.Features.Users.Queries.GetUserProfile;

namespace SocialMediaBackend.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly IMediator mediator;

        public UsersController(IMediator mediator)
        {
            this.mediator = mediator;
        }

        [HttpGet("GetUserProfile/{userName}")]
        public async Task<IActionResult> GetUserProfile([FromRoute]string userName)
        {
            var request = new GetUserProfileQueryRequest();
            request.UserName = userName;

            var response = await mediator.Send(request);
            return Ok(response);
        }
    }
}
