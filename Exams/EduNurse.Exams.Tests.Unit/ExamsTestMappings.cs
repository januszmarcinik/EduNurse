using EduNurse.Exams.Entities;
using EduNurse.Exams.Shared.Commands;

namespace EduNurse.Exams.Tests.Unit
{
    internal class ExamsTestMappings : ExamsMappings
    {
        public ExamsTestMappings()
        {
            CreateMap<Question, AddQuestionCommand>();
            CreateMap<Question, EditQuestionCommand>();
            CreateMap<Exam, AddExamCommand>();
            CreateMap<Exam, EditExamCommand>();
        }
    }
}
