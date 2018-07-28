using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using EduNurse.Api.Shared.Command;
using EduNurse.Exams.Shared.Enums;

namespace EduNurse.Exams.Shared.Commands
{
    public class AddExamCommand : ICommand
    {
        [Required]
        public string Name { get; set; }

        [Required]
        public ExamType Type { get; set; }

        [Required]
        public string Category { get; set; }

        [Required]
        public IEnumerable<AddQuestionCommand> Questions { get; set; }
    }
}
