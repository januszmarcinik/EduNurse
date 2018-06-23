using System;
using System.Collections.Generic;

namespace EduNurse.Exams.Api
{
    public interface IExamsContext : IDisposable
    {
        IEnumerable<T> GetAll<T>() where T : class;
        void Create<T>(T entity) where T : class;
        void CreateMany<T>(IEnumerable<T> entities) where T : class;
        void SaveChanges();
    }
}
