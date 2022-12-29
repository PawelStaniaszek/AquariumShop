using AquariumShop.Commands;
using Domain.Models;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Services.User;

namespace AquariumShop.Handlers
{
    public class RegisterHandler : IRequestHandler<RegisterCommand, IActionResult>
    {
        private readonly IAccountService _accountService;

        public RegisterHandler(IAccountService accountService)
        {
            _accountService = accountService;
        }

        public async Task<IActionResult> Handle(RegisterCommand request, CancellationToken cancellationToken)
        {
            ApiUser user = new()
            {
                FirstName = request.FirstName,
                LastName = request.LastName,
                Email = request.EmailAddress,
                UserName = request.EmailAddress
            };

            var identity = await _accountService.Register(user, request.Password);

            if (identity == null)
                return new BadRequestObjectResult("New user not created");
            else if (identity.Succeeded)
                return new OkResult();
            else
                return (IActionResult)identity.Errors;
        }
    }
}
