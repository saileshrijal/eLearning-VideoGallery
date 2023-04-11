using eLearning.Data;
using eLearning.Models;
using eLearning.Repository.Interface;

namespace eLearning.Repository
{
    public class UserRepository : Repository<ApplicationUser>, IUserRepository
    {
        public UserRepository(ApplicationDbContext context) : base(context)
        {
        }
    }
}
