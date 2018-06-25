using System;
using EduNurse.Exams.Shared.Questions;

namespace EduNurse.Exams.Api.Questions
{
    internal class Question : Entity
    {
        private Question()
        {
        }

        public Question(Guid id, string text, string a, string b, string c, string d, CorrectAnswer correctAnswer)
        {
            Id = id;
            Text = text;
            A = a;
            B = b;
            C = c;
            D = d;
            CorrectAnswer = correctAnswer;
        }

        public string Text { get; private set; }
        public string A { get; private set; }
        public string B { get; private set; }
        public string C { get; private set; }
        public string D { get; private set; }
        public CorrectAnswer CorrectAnswer { get; private set; }
    }
}
