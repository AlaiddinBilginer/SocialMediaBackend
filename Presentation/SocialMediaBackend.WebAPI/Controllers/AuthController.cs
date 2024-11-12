using MediatR;
using Microsoft.AspNetCore.Mvc;
using SocialMediaBackend.Application.Features.Auth.Commands.Register;

namespace SocialMediaBackend.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IMediator _mediator;

        public AuthController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost("Register")]
        public async Task<IActionResult> Register([FromBody]RegisterCommandRequest request)
        {
            RegisterCommandResponse response = await _mediator.Send(request);
            return Ok(response);
        }
    }
}
