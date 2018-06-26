using System;
using EduNurse.Exams.Shared.Enums;

namespace EduNurse.Exams.Shared.Dto
{
    public class ExamDto
    {
        public ExamDto(Guid id, string name, ExamType type, string category)
        {
            Id = id;
            Name = name;
            Type = type;
            Category = category;
        }

        public Guid Id { get; set; }
        public string Name { get; set; }
        public ExamType Type { get; set; }
        public string Category { get; set; }
    }
}
