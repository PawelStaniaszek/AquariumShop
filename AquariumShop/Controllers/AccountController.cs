using AquariumShop.Commands;
using AquariumShop.Dtos;
using MediatR;

using Microsoft.AspNetCore.Mvc;

namespace AquariumShop.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController
    {
        private readonly IMediator _mediator;

        public AccountController(IMediator mediator)
        {
            _mediator = mediator;
        }
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
