using AutoMapper;
using EduNurse.Exams.Entities;
using EduNurse.Exams.Shared.Commands;

namespace EduNurse.Exams.Tests.Unit
{
    internal class AutoMapperConfiguration
    {
        public static IMapper GetMapper()
        {
            return new MapperConfiguration(x =>
                {
                    x.AddProfile<ExamsAutoMapperProfile>();
                    x.CreateMap<Question, AddQuestionCommand>();
                    x.CreateMap<Question, EditQuestionCommand>();
                    x.CreateMap<Exam, AddExamCommand>();
                    x.CreateMap<Exam, EditExamCommand>();
                })
                .CreateMapper();
        }
    }
}
