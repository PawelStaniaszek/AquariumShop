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

        public Task<ActionResult<TokenDto>> Handle(LoginCommand request, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}
