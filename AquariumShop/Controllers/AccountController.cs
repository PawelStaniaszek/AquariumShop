using AquariumShop.Commands;
using AquariumShop.Dtos;
using MediatR;

using Microsoft.AspNetCore.Mvc;

namespace AquariumShop.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : BaseController
    {
        public AccountController(IMediator mediator) : base(mediator) { }

        [HttpPost("Register")]
        public async Task<IActionResult> Register([FromBody] RegisterCommand command)
        {
            return await _mediator.Send(command, CancellationToken.None);
        }

        [HttpPost("Login")]
        public async Task<ActionResult<TokenDto>> Login([FromBody] LoginCommand command)
        {
            return await _mediator.Send(command, CancellationToken.None);
        }
    }
}
