using eLearning.Dtos;

namespace eLearning.Service.Interface
{
    public interface IVideoService
    {
        //service for create, update and delete
        Task CreateVideo(VideoDto videoDto);
        Task UpdateVideo(int id, VideoDto videoDto);
        Task DeleteVideo(int id);
    }
}