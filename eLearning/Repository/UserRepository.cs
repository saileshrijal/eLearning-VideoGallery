using eLearning.Data;
using eLearning.Models;
using eLearning.Repository.Interface;
using Microsoft.EntityFrameworkCore;

namespace eLearning.Repository
{
    public class UserRepository : Repository<ApplicationUser>, IUserRepository
    {
        public UserRepository(ApplicationDbContext context) : base(context)
        {
        }

        public Task<List<ApplicationUser>> GetStudentsWithGrade()
        {
            return _context.ApplicationUsers!.Include(x => x.Grade).Where(x=>x.GradeId != null).ToListAsync()!;
        }

        public Task<ApplicationUser> GetStudentWithGradeById(string id)
        {
            return _context.ApplicationUsers!.Include(x => x.Grade).Where(x => x.GradeId != null).FirstOrDefaultAsync(x => x.Id == id)!;
        }
    }
}
