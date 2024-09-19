
using Microsoft.EntityFrameworkCore;
using ProficiencyTracker.Models;

namespace ProficiencyTracker.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options)
            : base(options)
        {
        }
        public DbSet<Student> Students { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<StudentProgress> StudentProgress { get; set; }
        public DbSet<GradeLevel> GradeLevels { get; set; }
        public DbSet<ProficiencyItem> ProficiencyItems { get; set; }
        public DbSet<Subject> Subjects { get; set; }
    }
}