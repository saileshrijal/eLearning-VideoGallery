using eLearning.Data;
using eLearning.Models;
using eLearning.Repository.Interface;
using Microsoft.EntityFrameworkCore;
using X.PagedList;

namespace eLearning.Repository
{
    public class VideoRepository : Repository<Video>, IVideoRepository
    {
        public VideoRepository(ApplicationDbContext context) : base(context)
        {
        }
        public async Task<IPagedList<Video>> GetAllVideos(string? search, int? page)
        {
            int pageSize = 5;
            int pageNumber = (page ?? 1);
            if (search == null)
            {
                return await _context.Videos!.OrderByDescending(x => x.CreatedAt).Include(x => x.Subject).Include(x => x.Grade).Include(x => x.Chapter).ToPagedListAsync(pageNumber, pageSize);
            }
            else
            {
                return await _context.Videos!.Where(x => x.Title!.Contains(search)).OrderByDescending(x => x.CreatedAt).Include(x => x.Subject).Include(x => x.Grade).Include(x => x.Chapter).ToPagedListAsync(pageNumber, pageSize);
            }
        }

        public async Task<Video> GetVideoById(int? id)
        {
            return await _context.Videos!.Include(x => x.Subject).Include(x => x.Grade).Include(x => x.Chapter).FirstOrDefaultAsync(x => x.Id == id)!;
        }

        public async Task<List<Video>> GetVideosByGradeIdSubjectIdChapterId(int gradeId, int subjectId, int chapterId)
        {
            return await _context.Videos!.Where(x => x.GradeId == gradeId & x.SubjectId == subjectId & x.ChapterId == chapterId).ToListAsync();
        }
        public async Task<Video> GetVideoById(int id)
        {
            return await _context.Videos!.Include(x => x.Subject).Include(x => x.Grade).Include(x => x.Chapter).FirstOrDefaultAsync(x => x.Id == id);
        }
    }
}