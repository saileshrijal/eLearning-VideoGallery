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
        public async Task<IPagedList<Grade>> GetAllGrades(string? search, int? page)
        {
            int pageSize = 4;
            int pageNumber = (page ?? 1);
            if (search == null)
            {
                return await _context.Grades!.OrderByDescending(x => x.CreatedAt).ToPagedListAsync(pageNumber, pageSize);
            }
            else
            {
                return await _context.Grades!.Where(x => x.Name!.Contains(search)).OrderByDescending(x => x.CreatedAt).ToPagedListAsync(pageNumber, pageSize);
            }
        }
    }
}