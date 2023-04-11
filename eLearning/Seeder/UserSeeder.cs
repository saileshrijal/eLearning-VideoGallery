using System.Transactions;
using eLearning.Constant;
using eLearning.Models;
using Microsoft.AspNetCore.Identity;

namespace eLearning.Seeder.Interface
{
    public class UserSeeder : IUserSeeder
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;

        public UserSeeder(UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager)
        {
            _userManager = userManager;
            _roleManager = roleManager;
        }
        public async Task SeedAdminUser()
        {
            var user = await _userManager.GetUsersInRoleAsync(UserRole.Admin);
            if (!user.Any())
            { //transaction scope
                var role = new IdentityRole(UserRole.Admin);
                await _roleManager.CreateAsync(role);

                var applicationUser = new ApplicationUser()
                {
                    UserName = "admin",
                    Email = "admin@gmail.com",
                    FirstName = "Super",
                    LastName = "Admin"
                };
                await _userManager.CreateAsync(applicationUser, "Admin@0011");
                await _userManager.AddToRoleAsync(applicationUser, UserRole.Admin);
            }
        }
    }
}