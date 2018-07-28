using EduNurse.Exams.Shared.Enums;
using EduNurse.Exams.Shared.Results;

namespace EduNurse.Exams.Shared.Queries
{
    public class GetExamsByTypeAndCategoryQuery : IQuery<ExamsResult>
    {
        public ExamType Type { get; set; }
        public string Category { get; set; }
    }
}
