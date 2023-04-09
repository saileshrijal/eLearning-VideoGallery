using eLearning.Models;
using X.PagedList;

namespace eLearning.Repository.Interface
{
    public interface IChapterRepository : IRepository<Chapter>
    {
        Task<IPagedList<Chapter>> GetAllChapters(string? search, int? page);
        Task<Chapter> GetChapterById(int? id);
    }
}