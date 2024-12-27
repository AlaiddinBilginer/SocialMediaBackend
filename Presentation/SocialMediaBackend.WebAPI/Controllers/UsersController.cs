using MediatR;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SocialMediaBackend.Application.Features.Users.Commands.UpdateUserProfile;
using SocialMediaBackend.Application.Features.Users.Queries.GetPostsByUser;
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

        [HttpPut("UpdateUser")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public async Task<IActionResult> UpdateUser([FromBody]UpdateUserProfileCommandRequest request)
        {
            var response = await mediator.Send(request);
            return Ok(response);
        }

        [HttpGet("GetPosts")]
        public async Task<IActionResult> GetPosts([FromQuery]GetPostsByUserQueryRequest request)
        {
            var response = await mediator.Send(request);
            return Ok(response);
        }
    }
}
