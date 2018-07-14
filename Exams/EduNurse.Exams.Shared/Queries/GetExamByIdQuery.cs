using System;

namespace EduNurse.Exams.Shared.Queries
{
    public class GetExamByIdQuery : IQuery
    {
        public Guid Id { get; set; }
    }
}
