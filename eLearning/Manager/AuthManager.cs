using eLearning.Models;
using eLearning.Results;
using Microsoft.AspNetCore.Identity;

namespace eLearning.Manager.Interface
{
    public class AuthManager : IAuthManager
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;

        public AuthManager(UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
        }

        public async Task<AuthResult> Login(string identity, string password)
        {
            var user = await _userManager.FindByNameAsync(identity);
            var result = new AuthResult();
            if (user == null)
            {
                result.Success = false;
                result.Errors.Add("User not found");
                return result;
            }
            if (!await _userManager.CheckPasswordAsync(user, password))
            {
                result.Success = false;
                result.Errors.Add("Password is incorrect");
                return result;
            }
            var res = await _signInManager.PasswordSignInAsync(identity, password, true, true);
            result.Success = true;
            return result;
        }

        public async Task Logout()
        {
            await _signInManager.SignOutAsync();
        }
    }
}