using eLearning.Dtos;
using eLearning.Models;
using eLearning.Repository.Interface;
using eLearning.Service.Interface;

namespace eLearning.Service
{
    public class ChapterService : IChapterService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IChapterRepository _chapterRepository;
        public ChapterService(IUnitOfWork unitOfWork, IChapterRepository chapterRepository)
        {
            _unitOfWork = unitOfWork;
            _chapterRepository = chapterRepository;
        }

        public async Task CreateChapter(ChapterDto chapterDto)
        {
            var chapter = new Chapter
            {
                Name = chapterDto.Name,
                Code = chapterDto.Code,
                Unit = chapterDto.Unit,
                Description = chapterDto.Description,
                SubjectId = chapterDto.SubjectId,
                GradeId = chapterDto.GradeId
            };
            await _unitOfWork.CreateAsync(chapter);
            await _unitOfWork.CommitAsync();
        }

        public async Task DeleteChapter(int id)
        {
            var chapter = await _chapterRepository.GetById(id);
            await _unitOfWork.DeleteAsync(chapter);
            await _unitOfWork.CommitAsync();
        }

        public async Task UpdateChapter(int id, ChapterDto chapterDto)
        {
            var chapter = await _chapterRepository.GetById(id);
            chapter.Name = chapterDto.Name;
            chapter.Description = chapterDto.Description;
            chapter.Code = chapterDto.Code;
            chapter.Unit = chapterDto.Unit;
            chapter.SubjectId = chapterDto.SubjectId;
            chapter.GradeId = chapterDto.GradeId;
            await _unitOfWork.CommitAsync();
        }
    }
}