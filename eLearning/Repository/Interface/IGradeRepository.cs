using eLearning.Models;
using X.PagedList;

namespace eLearning.Repository.Interface
{
    public interface IGradeRepository : IRepository<Grade>
    {
        Task<List<Grade>> SearchByName(string search);
    }
}