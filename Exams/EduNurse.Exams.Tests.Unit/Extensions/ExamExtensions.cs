using EduNurse.Exams.Entities;

namespace EduNurse.Exams.Tests.Unit.Extensions
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
    }
}
