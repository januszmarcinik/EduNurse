using AutoMapper;
using EduNurse.Exams.Entities;
using EduNurse.Exams.Shared.Results;

namespace EduNurse.Exams
{
    internal class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<Question, ExamWithQuestionsResult.Question>();
            CreateMap<Exam, ExamsResult.Exam>();
            CreateMap<Exam, ExamWithQuestionsResult>();
        }
    }
}
