using Microsoft.EntityFrameworkCore;
using eLearning.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

namespace eLearning.Data;

public class ApplicationDbContext : IdentityDbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
    {
    }

    public DbSet<Grade>? Grades { get; set; }
    public DbSet<Subject>? Subjects { get; set; }
    public DbSet<SubjectGrade>? SubjectGrades { get; set; }
    public DbSet<Chapter>? Chapters { get; set; }
    public DbSet<Video>? Videos { get; set; }
    public DbSet<ApplicationUser>? ApplicationUsers { get; set; }
}

