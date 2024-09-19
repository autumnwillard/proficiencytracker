
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using ProficiencyTracker.Models;

namespace ProficiencyTracker.Data
{
    public class AppDbContext : IdentityDbContext<User>
    {
        public AppDbContext(DbContextOptions<AppDbContext> options)
            : base(options)
        {
        }
        public DbSet<Student> Students { get; set; }
        public DbSet<StudentProgress> StudentProgress { get; set; }
        public DbSet<GradeLevel> GradeLevels { get; set; }
        public DbSet<ProficiencyItem> ProficiencyItems { get; set; }
        public DbSet<Subject> Subjects { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Student>()
                .HasOne(s => s.User)
                .WithMany(u => u.Students)
                .HasForeignKey(s => s.UserId);
        }
    }
}