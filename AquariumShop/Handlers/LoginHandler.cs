using AquariumShop.Commands;
using AquariumShop.Dtos;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Services.User;

namespace AquariumShop.Handlers
{
    public class LoginHandler : IRequestHandler<LoginCommand, ActionResult<TokenDto>>
    {
        private readonly IAccountService _accountService;

        public LoginHandler(IAccountService accountService)
        {
            _accountService = accountService;
        }

        public async Task<ActionResult<TokenDto>> Handle(LoginCommand request, CancellationToken cancellationToken)
        {
            var token = await _accountService.Login(request.Email, request.Password);

            if (token == null)
                return new BadRequestObjectResult("User not exists or password didn't match");

            return new OkObjectResult(new TokenDto() { Token = "Bearer " + token, });
        }
    }
}
