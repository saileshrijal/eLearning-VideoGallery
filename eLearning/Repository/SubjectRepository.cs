using eLearning.Data;
using eLearning.Models;
using eLearning.Repository.Interface;
using Microsoft.EntityFrameworkCore;
using X.PagedList;

namespace eLearning.Repository
{
    public class SubjectRepository : Repository<Subject>, ISubjectRepository
    {
        public SubjectRepository(ApplicationDbContext context) : base(context)
        {
        }
        public async Task<IPagedList<Subject>> GetAllSubjects(string? search, int? page)
        {
            int pageSize = 5;
            int pageNumber = (page ?? 1);
            if (search == null)
            {
                return await _context.Subjects!.OrderByDescending(x => x.CreatedAt).ToPagedListAsync(pageNumber, pageSize);
            }
            else
            {
                return await _context.Subjects!.Where(x => x.Name!.Contains(search)).OrderByDescending(x => x.CreatedAt).ToPagedListAsync(pageNumber, pageSize);
            }
        }
    }
}