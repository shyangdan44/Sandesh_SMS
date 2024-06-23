using Microsoft.AspNetCore.Identity;
using System.Data;

namespace Sandesh_SMS.Data
{
    public class DbSeeder
    {
        public static async Task SeedRolesAndAdminAsync(IServiceProvider service)
        {
            //seed Roles
            var userManager = service.GetService<UserManager<ApplicationUser>>();
            var roleManager = service.GetService<RoleManager<IdentityRole>>();
            await roleManager.CreateAsync(new IdentityRole(Roles.Admin.ToString()));
            await roleManager.CreateAsync(new IdentityRole(Roles.User.ToString()));

            //creating Admin
            var user = new ApplicationUser
            {

                Email = "admin@gmail.com",
                FullName = "Admin",
                EmailConfirmed = true

            };
            var userIdDb = await userManager.FindByEmailAsync(user.Email);
            if (userIdDb == null)
            {
                await userManager.CreateAsync(user, "Admin@123");
                await userManager.AddToRoleAsync(user, Roles.Admin.ToString());
            }

        }
    }
}
