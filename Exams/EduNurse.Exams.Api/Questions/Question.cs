using System;

namespace EduNurse.Exams.Api.Questions
{
    internal class Question : Entity
    {
        private Question()
        {
        }

        public Question(Guid id, string text)
        {
            Id = id;
            Text = text;
        }

        public string Text { get; private set; }
    }
}
