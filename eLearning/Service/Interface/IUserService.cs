using eLearning.Dtos;
using eLearning.Models;

namespace eLearning.Service.Interface
{
    public interface IUserService
    {
        Task<ApplicationUser> Create(UserDto userDto);
        Task Edit(string id, UserDto userDto);
    }
}
