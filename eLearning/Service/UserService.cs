using eLearning.Dtos;
using eLearning.Models;
using eLearning.Service.Interface;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.Transactions;

namespace eLearning.Service
{
    public class UserService : IUserService
    {
        private readonly UserManager<ApplicationUser> _userManager;

        public UserService(UserManager<ApplicationUser> userManager)
        {
            _userManager = userManager;
        }

        public async Task<ApplicationUser> Create(UserDto userDto)
        {
            using var tx = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled);
            await Validate(userDto.UserName, userDto.Email);
            var applicationUser = new ApplicationUser()
            {
                UserName = userDto.UserName,
                Email = userDto.Email,
                FirstName = userDto.FirstName,
                LastName = userDto.LastName,
                Address = userDto.Address,
                CreatedAt = DateTime.UtcNow,
                GradeId = userDto.GradeId,
            };
            await _userManager.CreateAsync(applicationUser, userDto.Password!);
            await _userManager.AddToRoleAsync(applicationUser, userDto.UserRole!);
            tx.Complete();
            return applicationUser;
        }

        public async Task Edit(string id, UserDto userDto)
        {
            var user = await _userManager.Users.FirstOrDefaultAsync(x=>x.Id == id);
            if(user == null)
            {
                throw new Exception("User not found");
            }
            user.FirstName = userDto.FirstName;
            user.LastName = userDto.LastName;
            user.Address = userDto.Address;
            user.GradeId = userDto.GradeId;
            await _userManager.UpdateAsync(user);
        }

        private async Task Validate(string? username, string? email)
        {
            if(await _userManager.FindByNameAsync(username!) != null)
            {
                throw new Exception($"Username {username} already taken. Please use another username");
            }
            if(await _userManager.FindByEmailAsync(email!) != null)
            {
                throw new Exception($"Email {email} already taken. Please use another email");
            }
        }
    }
}
