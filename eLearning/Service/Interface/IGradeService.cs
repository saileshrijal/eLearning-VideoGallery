using eLearning.Dtos;

namespace eLearning.Service.Interface
{
    public interface IGradeService
    {
        //service for create, update and delete
        Task CreateGrade(GradeDto gradeDto);
        Task UpdateGrade(int id, GradeDto gradeDto);
        Task DeleteGrade(int id);
    }
}