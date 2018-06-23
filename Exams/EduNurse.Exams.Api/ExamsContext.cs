using System;
using System.Collections.Generic;
using System.Linq;
using EduNurse.Exams.Api.Questions;
using Microsoft.EntityFrameworkCore;

namespace EduNurse.Exams.Api
{
    internal class ExamsContext : DbContext, IExamsContext
    {
        public ExamsContext(DbContextOptions options)
            : base(options)
        {
            
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Question>()
                .HasKey(x => x.Id);

            base.OnModelCreating(modelBuilder);
        }

        public IEnumerable<T> GetAll<T>() where T : class
        {
            return Set<T>().ToList();
        }

        public T GetById<T>(Guid id) where T : class
        {
            return Find<T>(id);
        }

        public void Create<T>(T entity) where T : class
        {
            Add(entity);
        }

        public void CreateMany<T>(IEnumerable<T> entities) where T : class
        {
            AddRange(entities);
        }

        public new void SaveChanges()
        {
            base.SaveChanges();
        }
    }
}
