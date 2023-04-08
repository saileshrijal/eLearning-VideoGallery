using Microsoft.EntityFrameworkCore;
using eLearning.Models;

namespace eLearning.Data;

public class ApplicationDbContext : DbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
    {
    }

    public DbSet<Grade>? Grades { get; set; }
    public DbSet<Subject>? Subjects { get; set; }
}

