using eLearning.Models;
using X.PagedList;

namespace eLearning.Repository.Interface
{
    public interface ISubjectGradeRepository : IRepository<SubjectGrade>
    {
        Task<List<SubjectGrade>> GetSubjectGrades();
        Task<List<Subject>> GetSubjectsByGradeId(int gradeId);
    }
}