using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace AquariumShop.Commands
{
    public class RegisterCommand : IRequest<IActionResult>
    {
        [FromBody]
        public string FirstName { get; set; }

        [FromBody]
        public string LastName { get; set; }

        [Required]
        [FromBody]
        public string EmailAddress { get; set; }

        [Required]
        [FromBody]
        public string Password { get; set; }
    }
}
