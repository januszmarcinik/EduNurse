using EduNurse.Exams.Shared.Enums;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using EduNurse.Api.Shared.Command;

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
        public IEnumerable<EditQuestionCommand> Questions { get; set; }
    }
}
