using EduNurse.Exams.Api.Entities;
using Microsoft.EntityFrameworkCore;

namespace EduNurse.Exams.Api
{
    internal class ExamsContext : DbContext
    {
        public DbSet<Exam> Exams { get; set; }
        public DbSet<Question> Questions { get; set; }

        public ExamsContext(DbContextOptions options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Exam>()
                .HasKey(x => x.Id);
            modelBuilder.Entity<Exam>()
                .HasMany<Question>(x => x.Questions)
                .WithOne(x => x.Exam)
                .HasForeignKey(x => x.ExamId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Question>()
                .HasKey(x => x.Id);

            base.OnModelCreating(modelBuilder);
        }
    }
}
