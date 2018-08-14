using System;
using EduNurse.Exams.Shared.Enums;

namespace EduNurse.Exams.Entities
{
    internal class Question
    {
        public Guid Id { get; }
        public Exam Exam { get; }

        public int Order { get; private set; }
        public string Text { get; private set; }
        public string A { get; private set; }
        public string B { get; private set; }
        public string C { get; private set; }
        public string D { get; private set; }
        public CorrectAnswer CorrectAnswer { get; private set; }
        public string Explanation { get; private set; }

        public Question(
            Guid id,
            Exam exam,
            int order,
            string text, 
            string a, 
            string b, 
            string c, 
            string d, 
            CorrectAnswer correctAnswer,
            string explanation
            )
        {
            Id = id;
            Exam = exam;
            Order = order;
            Text = text;
            A = a;
            B = b;
            C = c;
            D = d;
            CorrectAnswer = correctAnswer;
            Explanation = explanation;
        }

        public void SetOrder(int order)
        {
            Order = order;
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
