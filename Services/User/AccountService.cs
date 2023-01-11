using Domain.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Services.User
{
    public class AccountService : IAccountService
    {
        private readonly SignInManager<ApiUser> _signInManager;
        private readonly IConfiguration _configuration;

        protected readonly UserManager<ApiUser> _userManager;

        public AccountService(
            UserManager<ApiUser> userManager,
            SignInManager<ApiUser> signInManager,
            IConfiguration configuration)
        {
            _signInManager = signInManager;
            _configuration = configuration;
            _userManager = userManager;
        }

        private SigningCredentials GenerateSigningCredential()
        {
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]));

            return new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
        }

        public async Task<string> Login(string email, string password)
        {
            SignInResult loginResult;
            var user = await _userManager.FindByEmailAsync(email);
            if (user == null)
                return null;

            loginResult = await _signInManager.PasswordSignInAsync(user, password, false, false);

            if (loginResult.Succeeded)
            {
                var claims = GenerateClaims(user);

                var token = new JwtSecurityToken(
                        _configuration["Jwt:Issuer"],
                        _configuration["Jwt:Audience"],
                        claims,
                        notBefore: DateTime.Now,
                        expires: DateTime.Now.AddMinutes(30),
                        GenerateSigningCredential()
                    );
                return new JwtSecurityTokenHandler().WriteToken(token);
            }

            return null;
        }

        private Claim[] GenerateClaims(ApiUser user)
        {
            return new[]
            {
                new Claim(JwtRegisteredClaimNames.Sub, user.Id),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new Claim(JwtRegisteredClaimNames.UniqueName, user.Id)
            };
        }

        public async Task<IdentityResult> Register(ApiUser user, string password)
        {
            var result = await _userManager.CreateAsync(user, password);
            await _userManager.AddToRoleAsync(user, "User");
            return result;
        }
    }
}
