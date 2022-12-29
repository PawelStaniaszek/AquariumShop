using Domain.Models;
using Microsoft.AspNetCore.Identity;

namespace Services.User
{
    public interface IAccountService
    {
        Task<string> Login(string username, string password);

        Task<IdentityResult> Register(ApiUser user, string password);
    }
}
