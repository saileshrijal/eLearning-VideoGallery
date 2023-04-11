using eLearning.Models;
using X.PagedList;

namespace eLearning.Repository.Interface
{
    public interface IVideoRepository : IRepository<Video>
    {
        Task<IPagedList<Video>> GetAllVideos(string? search, int? page);
        Task<Video> GetVideoById(int? id);
    }
}