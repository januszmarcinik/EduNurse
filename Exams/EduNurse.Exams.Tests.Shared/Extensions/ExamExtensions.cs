using System.Linq;
using EduNurse.Exams.Entities;
using EduNurse.Exams.Shared.Commands;
using EduNurse.Exams.Shared.Results;

namespace EduNurse.Exams.Tests.Shared.Extensions
{
    internal static class ExamExtensions
    {
        internal static Exam DeepClone(this Exam exam)
        {
            var clone = new Exam(exam.Id, exam.Name, exam.Type, exam.Category, exam.CreatedBy, exam.CreatedDate, exam.IsConfirmed);
            foreach (var q in exam.Questions)
            {
                clone.AddQuestion(q.Order, q.Text, q.A, q.B, q.C, q.D, q.CorrectAnswer, q.Explanation);
            }

            return clone;
        }

        public static ExamsResult.Exam ToExamResult(this Exam exam)
        {
            return new ExamsResult.Exam()
            {
                Id = exam.Id,
                Name = exam.Name,
                Type = exam.Type,
                Category = exam.Category,
                CreatedBy = exam.CreatedBy,
                CreatedDate = exam.CreatedDate,
                IsConfirmed = exam.IsConfirmed
            };
        }

        public static ExamWithQuestionsResult ToExamWithQuestionsResult(this Exam exam)
        {
            return new ExamWithQuestionsResult()
            {
                Id = exam.Id,
                Name = exam.Name,
                Type = exam.Type,
                Category = exam.Category,
                CreatedBy = exam.CreatedBy,
                CreatedDate = exam.CreatedDate,
                IsConfirmed = exam.IsConfirmed,
                Questions = exam.Questions
                    .Select(q => q.ToQuestionResult())
            };
        }

        public static AddExamCommand ToAddExamCommand(this Exam exam)
        {
            return new AddExamCommand()
            {
                Type = exam.Type,
                Category = exam.Category,
                Name = exam.Name,
                Questions = exam.Questions
                    .Select(q => q.ToAddQuestionCommand())
            };
        }

        public static EditExamCommand ToEditExamCommand(this Exam exam)
        {
            return new EditExamCommand()
            {
                Type = exam.Type,
                Category = exam.Category,
                Name = exam.Name,
                Questions = exam.Questions
                    .Select(q => q.ToEditQuestionCommand())
            };
        }
    }
}
