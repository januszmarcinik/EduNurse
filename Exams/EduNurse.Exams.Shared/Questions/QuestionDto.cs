using System;

namespace EduNurse.Exams.Shared.Questions
{
    public class QuestionDto
    {
        public QuestionDto(Guid id, string text)
        {
            Id = id;
            Text = text;
        }

        public Guid Id { get; set; }
        public string Text { get; set; }
    }
}
