using System;
using System.Collections.Generic;

namespace EduNurse.Exams.Api
{
    internal interface IExamsContext : IDisposable
    {
        IEnumerable<T> GetAll<T>() where T : Entity;
        T GetById<T>(Guid id) where T : Entity;
        void Create<T>(T entity) where T : Entity;
        void CreateMany<T>(IEnumerable<T> entities) where T : Entity;
        void SaveChanges();
        void Update<T>(T entity) where T : Entity;
        void Delete<T>(Guid id) where T : Entity;
    }
}
