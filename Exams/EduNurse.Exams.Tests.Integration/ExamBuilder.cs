using System;
using EduNurse.Exams.Entities;
using EduNurse.Exams.Shared.Enums;

namespace EduNurse.Exams.Tests.Integration
{
    internal class ExamBuilder
    {
        private readonly Exam _prototype;

        public ExamBuilder(string name, ExamType type, string category)
        {
            _prototype = new Exam(
                id: Guid.NewGuid(),
                name: name,
                type: type,
                category: category,
                createdBy: Guid.NewGuid().ToString(),
                createdDate: DateTime.Now,
                isConfirmed: false
            );
        }

        public ExamBuilder WithQuestion(string text, CorrectAnswer correctAnswer)
        {
            _prototype.AddQuestion(
                order: _prototype.Questions.Count + 1,
                text: text, 
                a: "a1", 
                b: "b1", 
                c: "c1", 
                d: "d1", 
                correctAnswer: correctAnswer, 
                explanation: Guid.NewGuid().ToString()
            );

            return this;
        }

        public Exam Build()
        {
            return _prototype;
        }
    }
}
