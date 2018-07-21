using AutoMapper;
using EduNurse.Exams.Api.Entities;
using EduNurse.Exams.Shared.Results;

namespace EduNurse.Exams.Api
{
    internal class AutoMapperConfig
    {
        public static IMapper Initialize()
        {
            return new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<Question, QuestionResult>();
                cfg.CreateMap<Exam, ExamResult>();
                cfg.CreateMap<Exam, ExamWithQuestionsResult>();
            })
            .CreateMapper();
        }
    }
}
