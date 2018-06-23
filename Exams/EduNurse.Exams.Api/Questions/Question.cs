using System;

namespace EduNurse.Exams.Api.Questions
{
    public class Question
    {
        private Question()
        {
        }

        public Question(Guid id, string text)
        {
            Id = id;
            Text = text;
        }

        public Guid Id { get; private set; }
        public string Text { get; private set; }
    }
}
