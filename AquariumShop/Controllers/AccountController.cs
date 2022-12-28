using AquariumShop.Commands;
using AquariumShop.Dtos;
using AutoMapper;
using Domain.Models;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace AquariumShop.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController
    {
        private readonly UserManager<ApiUser> _userManager;
        private readonly SignInManager<ApiUser> _signInManager;
        private readonly IMediator _mediator;

        public AccountController(UserManager<ApiUser> userManager, SignInManager<ApiUser> signInManager, IMediator mediator)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _mediator = mediator;
        }
        [HttpPost("Create")]
        public async Task<IActionResult> Register([FromBody] RegisterCommand command)
        {
            return await _mediator.Send(command, CancellationToken.None);
        }

        [HttpGet("Login")]
        public async Task<ActionResult<TokenDto>> Login([FromBody] LoginCommand command)
        {
            return await _mediator.Send(command, CancellationToken.None);
        }
    }
}
