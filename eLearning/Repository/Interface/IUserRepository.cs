using eLearning.Models;

namespace eLearning.Repository.Interface
{
    public interface IUserRepository : IRepository<ApplicationUser>
    {
        Task<ApplicationUser> GetStudentWithGradeById(string id);
        Task<List<ApplicationUser>> GetStudentsWithGrade();
    }
}
