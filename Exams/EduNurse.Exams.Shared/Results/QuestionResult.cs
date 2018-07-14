using System;
using EduNurse.Exams.Shared.Enums;

namespace EduNurse.Exams.Shared.Results
{
    public class QuestionResult
    {
        public Guid Id { get; set; }
        public Guid ExamId { get; set; }
        public string Text { get; set; }
        public string A { get; set; }
        public string B { get; set; }
        public string C { get; set; }
        public string D { get; set; }
        public CorrectAnswer CorrectAnswer { get; set; }
        public string Explanation { get; set; }
    }
}
