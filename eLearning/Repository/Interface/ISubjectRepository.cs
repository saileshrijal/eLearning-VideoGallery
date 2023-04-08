using eLearning.Models;
using X.PagedList;

namespace eLearning.Repository.Interface
{
    public interface ISubjectRepository : IRepository<Subject>
    {
        Task<IPagedList<Subject>> GetAllSubjects(string? search, int? page);
    }
}