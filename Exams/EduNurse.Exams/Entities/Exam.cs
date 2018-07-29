using System;
using System.Collections.Generic;
using System.Linq;
using EduNurse.Exams.Shared.Enums;

namespace EduNurse.Exams.Entities
{
    internal class Exam : Entity
    {
        public string Name { get; private set; }
        public ExamType Type { get; private set; }
        public string Category { get; private set; }
        public string CreatedBy { get; private set; }
        public DateTime CreatedDate { get; private set; }
        public bool IsConfirmed { get; private set; }

        private readonly List<Question> _questions = new List<Question>();
        public IReadOnlyCollection<Question> Questions => _questions;

        public Exam(
            Guid id, 
            string name, 
            ExamType type, 
            string category, 
            string createdBy, 
            DateTime createdDate, 
            bool isConfirmed
            ) : base(id)
        {
            Name = name;
            Type = type;
            Category = category;
            CreatedBy = createdBy;
            CreatedDate = createdDate;
            IsConfirmed = isConfirmed;
        }

        public void SetName(string name)
        {
            Name = name;
        }

        public void ChangeType(ExamType type)
        {
            Type = type;
        }

        public void SetCategory(string category)
        {
            Category = category;
        }

        public void Confirm()
        {
            IsConfirmed = true;
        }

        public void AddQuestion(int order, string text, string a, string b, string c, string d, CorrectAnswer correctAnswer, string explanation)
        {
            _questions.Add(new Question(Guid.NewGuid(), Id, order, text, a, b, c, d, correctAnswer, explanation));
        }

        public void RemoveQuestion(Guid id)
        {
            var question = _questions.SingleOrDefault(x => x.Id == id);
            if (question == null)
            {
                throw new NullReferenceException($"Question with given Id '{id}' does not exits.");
            }

            _questions.Remove(question);
        }

        public Exam Clone()
        {
            return (Exam)MemberwiseClone();
        }
    }
}
