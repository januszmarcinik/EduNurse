using EduNurse.Exams.Shared.Enums;

namespace EduNurse.Exams.Shared.Queries
{
    public class GetExamsByTypeAndCategoryQuery : IQuery
    {
        public ExamType Type { get; set; }
        public string Category { get; set; }
    }
}
