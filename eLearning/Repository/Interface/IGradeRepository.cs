using eLearning.Models;
using X.PagedList;

namespace eLearning.Repository.Interface
{
    public interface IGradeRepository : IRepository<Grade>
    {
        Task<IPagedList<Grade>> GetAllGrades(string? search, int? page);
        Task<IPagedList<Grade>> GetAllGradesWithSubjects(string? search, int? page);
        Task<Grade> GetGradeWithSubjects(int id);

    }
}