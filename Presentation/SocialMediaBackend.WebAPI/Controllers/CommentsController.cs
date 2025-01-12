using MediatR;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SocialMediaBackend.Application.Features.Comments.Commands.CreateComment;
using SocialMediaBackend.Application.Features.Comments.Commands.DeleteComment;
using SocialMediaBackend.Application.Features.Comments.Commands.LikeComment;
using SocialMediaBackend.Application.Features.Comments.Commands.UpdateComment;
using SocialMediaBackend.Application.Features.Comments.Queries.GetCommentsByPostId;
using SocialMediaBackend.Application.Features.Comments.Queries.GetRepliesByParentCommentId;

namespace SocialMediaBackend.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CommentsController : ControllerBase
    {
        private readonly IMediator mediator;

        public CommentsController(IMediator mediator)
        {
            this.mediator = mediator;
        }

        [HttpPost("CreateComment")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public async Task<IActionResult> CreateComment([FromBody]CreateCommentCommandRequest request)
        {
            var response = await mediator.Send(request);
            return Ok(response);
        }

        [HttpGet("GetCommentsByPostId")]
        [Authorize(AuthenticationSchemes =JwtBearerDefaults.AuthenticationScheme)]
        public async Task<IActionResult> GetCommentsByPostId([FromQuery]GetCommentsByPostIdQueryRequest request)
        {
            var response = await mediator.Send(request);
            return Ok(response);
        }

        [HttpPut("UpdateComment")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public async Task<IActionResult> UpdateComment([FromBody]UpdateCommentCommandRequest request)
        {
            var response = await mediator.Send(request);
            return Ok(response);
        }

        [HttpDelete("DeleteComment/{id}")]
        [Authorize(AuthenticationSchemes =JwtBearerDefaults.AuthenticationScheme)]
        public async Task<IActionResult> DeleteComment([FromRoute]string id)
        {
            DeleteCommentCommandRequest request = new DeleteCommentCommandRequest();
            request.Id = id;
            var response = await mediator.Send(request);
            return Ok(response);
        }

        [HttpGet("GetRepliesByParentComment")]
                [Authorize(AuthenticationSchemes =JwtBearerDefaults.AuthenticationScheme)]
        public async Task<IActionResult> GetRepliesByParentComment([FromQuery]GetRepliesByParentCommentIdQueryRequest request)
        {
            var response = await mediator.Send(request);
            return Ok(response);
        }

        [HttpPost("LikeComment")]
        [Authorize(AuthenticationSchemes =JwtBearerDefaults.AuthenticationScheme)]
        public async Task<IActionResult> LikeComment([FromBody]LikeCommentCommandRequest request)
        {
            var response = await mediator.Send(request);
            return Ok(response);
        }
    }
}
