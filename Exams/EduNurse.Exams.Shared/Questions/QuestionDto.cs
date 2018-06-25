using System;

namespace EduNurse.Exams.Shared.Questions
{
    public class QuestionDto
    {
        public QuestionDto(Guid id, string text, string a, string b, string c, string d, CorrectAnswer correctAnswer)
        {
            Id = id;
            Text = text;
            A = a;
            B = b;
            C = c;
            D = d;
            CorrectAnswer = correctAnswer;
        }

        public Guid Id { get; set; }
        public string Text { get; set; }
        public string A { get; set; }
        public string B { get; set; }
        public string C { get; set; }
        public string D { get; set; }
        public CorrectAnswer CorrectAnswer { get; set; }
    }
}
