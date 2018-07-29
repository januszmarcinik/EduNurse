using System;

namespace EduNurse.Exams
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
