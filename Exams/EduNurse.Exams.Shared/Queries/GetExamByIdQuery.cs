using System;
using EduNurse.Api.Shared.Query;
using EduNurse.Exams.Shared.Results;

namespace EduNurse.Exams.Shared.Queries
{
    public class GetExamByIdQuery : IQuery<ExamWithQuestionsResult>
    {
        public Guid Id { get; set; }
    }
}
