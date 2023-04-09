using eLearning.Data;
using eLearning.Models;
using eLearning.Repository.Interface;
using Microsoft.EntityFrameworkCore;
using X.PagedList;

namespace eLearning.Repository
{
    public class SubjectGradeRepository : Repository<SubjectGrade>, ISubjectGradeRepository
    {
        public SubjectGradeRepository(ApplicationDbContext context) : base(context)
        {
        }

        public async Task<List<SubjectGrade>> GetSubjectGrades()
        {
            return await _context.SubjectGrades!.Include(s => s.Grade).Include(s => s.Subject).ToListAsync();
        }
    }
}