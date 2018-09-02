using AutoMapper;
using EduNurse.Exams.AzureTableStorage;
using EduNurse.Exams.Entities;
using EduNurse.Exams.Shared.Results;

namespace EduNurse.Exams
{
    internal class ExamsMappings : Profile
    {
        public ExamsMappings()
        {
            CreateMap<Question, ExamWithQuestionsResult.Question>();
            CreateMap<Question, AtsQuestion>()
                .ForMember(x => x.PartitionKey, e => e.UseValue(AtsExamsContext.QuestionsPartitionKey))
                .ForMember(x => x.RowKey, e => e.MapFrom(m => m.Id))
                .ForMember(x => x.Timestamp, e => e.Ignore())
                .ForMember(x => x.ETag, e => e.UseValue("*"));

            CreateMap<Exam, ExamsResult.Exam>();
            CreateMap<Exam, ExamWithQuestionsResult>();
            CreateMap<Exam, AtsExam>()
                .ForMember(x => x.PartitionKey, e => e.UseValue(AtsExamsContext.ExamsPartitionKey))
                .ForMember(x => x.RowKey, e => e.MapFrom(m => m.Id))
                .ForMember(x => x.Timestamp, e => e.Ignore())
                .ForMember(x => x.ETag, e => e.UseValue("*"));
        }
    }
}
