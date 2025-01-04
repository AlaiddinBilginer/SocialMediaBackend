using MediatR;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SocialMediaBackend.Application.Features.Posts.Commands.CreatePost;
using SocialMediaBackend.Application.Features.Posts.Commands.DeletePost;
using SocialMediaBackend.Application.Features.Posts.Commands.LikePost;
using SocialMediaBackend.Application.Features.Posts.Commands.UpdatePost;
using SocialMediaBackend.Application.Features.Posts.Queries.GetByIdPost;
using SocialMediaBackend.Application.Features.Posts.Queries.GetPostsByCategory;
using SocialMediaBackend.Application.Features.Posts.Queries.GetPostsByUser;

namespace SocialMediaBackend.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PostsController : ControllerBase
    {
        private readonly IMediator mediator;

        public PostsController(IMediator mediator)
        {
            this.mediator = mediator;
        }

        [HttpPost("Create")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public async Task<IActionResult> Create([FromForm]CreatePostCommandRequest request)
        {
            var response = await mediator.Send(request);
            return Ok(response);
        }

        [HttpGet("GetPostsByUser")]
        public async Task<IActionResult> GetPostsByUser([FromQuery]GetPostsByUserQueryRequest request)
        {
            var response = await mediator.Send(request);
            return Ok(response);
        }

        [HttpGet("GetById")]
        public async Task<IActionResult> GetById([FromQuery]string id)
        {
            var request = new GetByIdPostQueryRequest();
            request.Id = id;
            var response = await mediator.Send(request);
            return Ok(response);
        }

        [HttpPut("Update")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public async Task<IActionResult> Update([FromBody] UpdatePostCommandRequest request)
        {
            var response = await mediator.Send(request);
            return Ok(response);
        }

        [HttpDelete("Delete/{id}")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public async Task<IActionResult> Delete([FromRoute]string id)
        {
            var request = new DeletePostCommandRequest();
            request.Id = id;

            var response = await mediator.Send(request);
            return Ok(response);
        }

        [HttpGet("GetPostsByCategory")]
        public async Task<IActionResult> GetPostsByCategory([FromQuery]GetPostsByCategoryQueryRequest request)
        {
            var response = await mediator.Send(request);
            return Ok(response);
        }

        [HttpPost("LikePost")]
        public async Task<IActionResult> LikePost([FromBody]LikePostCommandRequest request)
        {
            var response = await mediator.Send(request);
            return Ok(response);
        }
    }
}
