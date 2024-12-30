using MediatR;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SocialMediaBackend.Application.Features.Users.Commands.FollowUser;
using SocialMediaBackend.Application.Features.Users.Commands.UnfollowUser;
using SocialMediaBackend.Application.Features.Users.Commands.UpdateUserProfile;
using SocialMediaBackend.Application.Features.Users.Queries.GetCommentsByUser;
using SocialMediaBackend.Application.Features.Users.Queries.GetPostsByUser;
using SocialMediaBackend.Application.Features.Users.Queries.GetUserProfile;
using SocialMediaBackend.Application.Features.Users.Queries.GetFollowers;
using SocialMediaBackend.Application.Features.Users.Queries.GetFollowing;

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
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
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
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public async Task<IActionResult> GetPosts([FromQuery]GetPostsByUserQueryRequest request)
        {
            var response = await mediator.Send(request);
            return Ok(response);
        }

        [HttpPost("Follow/{followedUserName}")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public async Task<IActionResult> Follow([FromRoute]string followedUserName)
        {
            var request = new FollowUserCommandRequest();
            request.FollowedUserName = followedUserName;

            var response = await mediator.Send(request);
            return Ok(response);
        }

        [HttpPost("Unfollow/{unfollowedUserName}")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public async Task<IActionResult> Unfollow([FromRoute]string unfollowedUserName)
        {
            var request = new UnfollowUserCommandRequest();
            request.UnfollowedUserName = unfollowedUserName;

            var response = await mediator.Send(request);
            return Ok(response);
        }

        [HttpGet("GetComments")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public async Task<IActionResult> GetComments([FromQuery]GetCommentsByUserQueryRequest request)
        {
            var response = await mediator.Send(request);
            return Ok(response);
        }

        [HttpGet("GetFollowers")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public async Task<IActionResult> GetFollowers([FromQuery]GetFollowersQueryRequest request)
        {
            var response = await mediator.Send(request);
            return Ok(response);
        }

        [HttpGet("GetFollowing")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public async Task<IActionResult> GetFollowing([FromQuery]GetFollowingQueryRequest request)
        {
            var response = await mediator.Send(request);
            return Ok(response);
        }
    }
}
