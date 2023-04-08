using eLearning.Dtos;

namespace eLearning.Service.Interface
{
    public interface ISubjectService
    {
        //service for create, update and delete
        Task CreateSubject(SubjectDto subjectDto);
        Task UpdateSubject(int id, SubjectDto subjectDto);
        Task DeleteSubject(int id);
    }
}