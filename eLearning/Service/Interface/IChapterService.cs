using eLearning.Dtos;

namespace eLearning.Service.Interface
{
    public interface IChapterService
    {
        //service for create, update and delete
        Task CreateChapter(ChapterDto chapterDto);
        Task UpdateChapter(int id, ChapterDto chapterDto);
        Task DeleteChapter(int id);
    }
}