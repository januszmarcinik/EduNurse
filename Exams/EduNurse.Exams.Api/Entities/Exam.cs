using System;
using EduNurse.Exams.Shared.Enums;

namespace EduNurse.Exams.Api.Entities
{
    internal class Exam : Entity
    {
        public Exam(Guid id, string name, ExamType type, string category) : base(id)
        {
            Name = name;
            Type = type;
            Category = category;
        }

        public string Name { get; private set; }
        public ExamType Type { get; private set; }
        public string Category { get; private set; }
    }
}
