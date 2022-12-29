using Microsoft.AspNetCore.Identity;

namespace AquariumShop.Seed
{
    public class RoleSeeder
    {
        public async Task Seed(IServiceProvider serviceProvider)
        {
            var roleManager = serviceProvider.GetRequiredService<RoleManager<IdentityRole>>();
            var anyRoles = roleManager.Roles;
            if (!anyRoles.Any())
            {
                await roleManager.CreateAsync(new IdentityRole
                {
                    Name = "User"
                });

                await roleManager.CreateAsync(new IdentityRole
                {
                    Name = "Admin"
                });
            }
        }
    }
}
