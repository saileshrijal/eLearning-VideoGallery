using eLearning.Data;
using Microsoft.EntityFrameworkCore;

namespace eLearning.Repository.Interface
{
    public class Repository<T> : IRepository<T> where T : class
    {
        protected ApplicationDbContext _context;
        private readonly DbSet<T> _dbSet;
        public Repository(ApplicationDbContext context)
        {
            _context = context;
            _dbSet = _context.Set<T>();
        }
        public async Task<List<T>> GetAll()
        {
            return await _dbSet.ToListAsync();
        }
        public async Task<T> GetById(int id)
        {
            return await _dbSet.FindAsync(id) ?? throw new Exception("Not found");
        }
    }
}