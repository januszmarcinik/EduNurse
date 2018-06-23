using System;

namespace EduNurse.Exams.Api
{
    internal abstract class Entity
    {
        public Guid Id { get; protected set; }
    }
}
