using System;
using EduNurse.Exams.Shared.Enums;

namespace EduNurse.Exams.Api.Entities
{
    internal class Question : Entity
    {
        public Guid ExamId { get; private set; }
        public Exam Exam { get; private set; }

        public string Text { get; private set; }
        public string A { get; private set; }
        public string B { get; private set; }
        public string C { get; private set; }
        public string D { get; private set; }
        public CorrectAnswer CorrectAnswer { get; private set; }
        public string Explanation { get; private set; }

        public Question(
            Guid id,
            Guid examId,
            string text, 
            string a, 
            string b, 
            string c, 
            string d, 
            CorrectAnswer correctAnswer,
            string explanation
            )
            : base(id)
        {
            ExamId = examId;
            Text = text;
            A = a;
            B = b;
            C = c;
            D = d;
            CorrectAnswer = correctAnswer;
            Explanation = explanation;
        }

        public void SetText(string text)
        {
            Text = text;
        }

        public void SetAnswers(string a, string b, string c, string d)
        {
            A = a;
            B = b;
            C = c;
            D = d;
        }

        public void ChangeCorrectAnswer(CorrectAnswer correctAnswer)
        {
            CorrectAnswer = correctAnswer;
        }

        public void SetExplantation(string explantation)
        {
            Explanation = explantation;
        }
    }
}
