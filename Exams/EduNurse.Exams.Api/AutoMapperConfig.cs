using AutoMapper;
using EduNurse.Exams.Api.Entities;
using EduNurse.Exams.Shared.Dto;

namespace EduNurse.Exams.Api
{
    internal class AutoMapperConfig
    {
        public static IMapper Initialize()
        {
            return new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<Question, QuestionDto>();
                cfg.CreateMap<Exam, ExamDto>();
            })
            .CreateMapper();
        }
    }
}
