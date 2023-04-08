using eLearning.Data;
using eLearning.Models;
using eLearning.Repository.Interface;
using Microsoft.EntityFrameworkCore;
using X.PagedList;

namespace eLearning.Repository
{
    public class GradeRepository : Repository<Grade>, IGradeRepository
    {
        public GradeRepository(ApplicationDbContext context) : base(context)
        {
        }

        public async Task<List<Grade>> SearchByName(string search)
        {
            return await _context.Grades!.Where(x => x.Name!.Contains(search)).ToListAsync();
        }
    }
}