using eLearning.Dtos;
using eLearning.Models;
using eLearning.Repository.Interface;
using eLearning.Service.Interface;

namespace eLearning.Service
{
    public class VideoService : IVideoService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IVideoRepository _videoRepository;
        public VideoService(IUnitOfWork unitOfWork, IVideoRepository videoRepository)
        {
            _unitOfWork = unitOfWork;
            _videoRepository = videoRepository;
        }

        public async Task CreateVideo(VideoDto videoDto)
        {
            var video = new Video
            {
                Title = videoDto.Title,
                Description = videoDto.Description,
                SubjectId = videoDto.SubjectId,
                GradeId = videoDto.GradeId,
                ChapterId = videoDto.ChapterId,
                ThumbnailUrl = videoDto.ThumbnailUrl,
                VideoUrl = videoDto.VideoUrl
            };
            await _unitOfWork.CreateAsync(video);
            await _unitOfWork.CommitAsync();
        }

        public async Task DeleteVideo(int id)
        {
            var video = await _videoRepository.GetById(id);
            await _unitOfWork.DeleteAsync(video);
            await _unitOfWork.CommitAsync();
        }

        public async Task UpdateVideo(int id, VideoDto videoDto)
        {
            var video = await _videoRepository.GetById(id);
            video.Title = videoDto.Title;
            video.Description = videoDto.Description;
            video.ChapterId = videoDto.ChapterId;
            video.SubjectId = videoDto.SubjectId;
            video.GradeId = videoDto.GradeId;
            video.ThumbnailUrl = videoDto.ThumbnailUrl;
            video.VideoUrl = videoDto.VideoUrl;
            await _unitOfWork.CommitAsync();
        }
    }
}