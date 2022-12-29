using AquariumShop.Dtos;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace AquariumShop.Commands
{
    public class LoginCommand : IRequest<ActionResult<TokenDto>>
    {
        public string Email { get; set; }
        public string Password { get; set; }
    }
}
