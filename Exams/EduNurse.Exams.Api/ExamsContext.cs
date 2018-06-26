using System;
using System.Collections.Generic;
using System.Linq;
using EduNurse.Exams.Api.Entities;
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

            modelBuilder.Entity<Exam>()
                .HasKey(x => x.Id);

            base.OnModelCreating(modelBuilder);
        }

        public IEnumerable<T> GetAll<T>() where T : Entity
        {
            return Set<T>().ToList();
        }

        public T GetById<T>(Guid id) where T : Entity
        {
            return Set<T>().SingleOrDefault(x => x.Id == id);
        }

        public void Create<T>(T entity) where T : Entity
        {
            Add(entity);
        }

        public void CreateMany<T>(IEnumerable<T> entities) where T : Entity
        {
            AddRange(entities);
        }

        public new void Update<T>(T entity) where T : Entity
        {
            base.Update(entity);
        }

        public void Delete<T>(Guid id) where T : Entity
        {
            var entity = GetById<T>(id);
            Remove(entity);
        }

        public new void SaveChanges()
        {
            base.SaveChanges();
        }
    }
}
