using System;
using System.ComponentModel.DataAnnotations;
using EduNurse.Exams.Shared.Enums;

namespace EduNurse.Exams.Shared.Commands
{
    public class EditQuestionCommand : ICommand
    {
        public Guid? Id { get; set; }

        [Required]
        public string Text { get; set; }

        [Required]
        public string A { get; set; }

        [Required]
        public string B { get; set; }

        [Required]
        public string C { get; set; }

        [Required]
        public string D { get; set; }

        [Required]
        public CorrectAnswer CorrectAnswer { get; set; }

        public string Explanation { get; set; }
    }
}
