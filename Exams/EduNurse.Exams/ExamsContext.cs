using EduNurse.Exams.Entities;
using Microsoft.EntityFrameworkCore;

namespace EduNurse.Exams
{
    internal class ExamsContext : DbContext
    {
        public DbSet<Exam> Exams { get; set; }
        public DbSet<Question> Questions { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Exam>()
                .HasKey(x => x.Id);
            modelBuilder.Entity<Exam>()
                .HasMany(x => x.Questions)
                .WithOne(x => x.Exam)
                .HasForeignKey(x => x.ExamId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Question>()
                .HasKey(x => x.Id);

            base.OnModelCreating(modelBuilder);
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (Settings.UseInMemory)
            {
                optionsBuilder.UseInMemoryDatabase(Settings.ConnectionString);
            }
            else
            {
                optionsBuilder.UseSqlServer(Settings.ConnectionString);
            }
        }
    }
}
