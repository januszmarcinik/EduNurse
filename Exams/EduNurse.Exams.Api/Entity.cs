using System;

namespace EduNurse.Exams.Api
{
    internal abstract class Entity
    {
        private Entity()
        {
        }

        protected Entity(Guid id)
        {
            Id = id;
        }

        public Guid Id { get; private set; }
    }
}
