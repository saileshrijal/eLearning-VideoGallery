using eLearning.Models;

namespace eLearning.Repository.Interface
{
    public interface IGradeRepository : IRepository<Grade>
    {
        Task<List<Grade>> SearchByName(string search);
    }
}