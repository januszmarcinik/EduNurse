using System.ComponentModel.DataAnnotations;
using EduNurse.Exams.Shared.Enums;

namespace EduNurse.Exams.Shared.Queries
{
    public class GetCategoriesByTypeQuery : IQuery
    {
        [Required]
        public ExamType Type { get; set; }
    }
}
