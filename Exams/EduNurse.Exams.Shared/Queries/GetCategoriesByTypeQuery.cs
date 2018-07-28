using System.ComponentModel.DataAnnotations;
using EduNurse.Api.Shared.Query;
using EduNurse.Exams.Shared.Enums;
using EduNurse.Exams.Shared.Results;

namespace EduNurse.Exams.Shared.Queries
{
    public class GetCategoriesByTypeQuery : IQuery<CategoriesResult>
    {
        [Required]
        public ExamType Type { get; set; }
    }
}
