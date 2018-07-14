using EduNurse.Exams.Shared.Enums;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using EduNurse.Exams.Shared.Results;

namespace EduNurse.Exams.Shared.Commands
{
    public class EditExamCommand : ICommand
    {
        [Required]
        public string Name { get; set; }

        [Required]
        public ExamType Type { get; set; }

        [Required]
        public string Category { get; set; }

        [Required]
        public IEnumerable<QuestionResult> Questions { get; set; }
    }
}
