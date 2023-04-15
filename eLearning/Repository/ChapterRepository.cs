using eLearning.Data;
using eLearning.Models;
using eLearning.Repository.Interface;
using Microsoft.EntityFrameworkCore;
using X.PagedList;

namespace eLearning.Repository
{
    public class ChapterRepository : Repository<Chapter>, IChapterRepository
    {
        public ChapterRepository(ApplicationDbContext context) : base(context)
        {
        }
        public async Task<IPagedList<Chapter>> GetAllChapters(string? search, int? page)
        {
            int pageSize = 5;
            int pageNumber = (page ?? 1);
            if (search == null)
            {
                return await _context.Chapters!.OrderByDescending(x => x.CreatedAt).Include(x => x.Subject).Include(x => x.Grade).ToPagedListAsync(pageNumber, pageSize);
            }
            else
            {
                return await _context.Chapters!.Where(x => x.Name!.Contains(search)).OrderByDescending(x => x.CreatedAt).Include(x => x.Subject).Include(x => x.Grade).ToPagedListAsync(pageNumber, pageSize);
            }
        }

        public Task<Chapter> GetChapterById(int? id)
        {
            return _context.Chapters!.Include(x => x.Subject).Include(x => x.Grade).FirstOrDefaultAsync(x => x.Id == id)!;
        }

        //get chapters by subject id
        public Task<List<Chapter>> GetChaptersBySubjectIdAndGradeId(int gradeId, int subjectId)
        {
            return _context.Chapters!.Include(x=>x.Subject)!.Where(x => x.SubjectId == subjectId && x.GradeId == gradeId).ToListAsync()!;
        }
    }
}