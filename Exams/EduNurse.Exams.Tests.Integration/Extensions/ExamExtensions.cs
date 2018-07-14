using System.Linq;
using EduNurse.Exams.Api.Entities;
using EduNurse.Exams.Shared.Commands;
using EduNurse.Exams.Shared.Results;

namespace EduNurse.Exams.Tests.Integration.Extensions
{
    internal static class ExamExtensions
    {
        public static ExamResult ToExamResult(this Exam exam)
        {
            return new ExamResult()
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
                    .Select(q => q.ToQuestionResult())
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
                    .Select(q => q.ToQuestionResult())
            };
        }
    }
}
