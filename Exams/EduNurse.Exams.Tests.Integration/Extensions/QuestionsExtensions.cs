using EduNurse.Exams.Entities;
using EduNurse.Exams.Shared.Commands;
using EduNurse.Exams.Shared.Results;

namespace EduNurse.Exams.Tests.Integration.Extensions
{
    internal static class QuestionsExtensions
    {
        public static ExamWithQuestionsResult.Question ToQuestionResult(this Question question)
        {
            return new ExamWithQuestionsResult.Question()
            {
                Id = question.Id,
                Order = question.Order,
                Text = question.Text,
                ExamId = question.ExamId,
                A = question.A,
                B = question.B,
                C = question.C,
                D = question.D,
                CorrectAnswer = question.CorrectAnswer,
                Explanation = question.Explanation
            };
        }

        public static AddQuestionCommand ToAddQuestionCommand(this Question question)
        {
            return new AddQuestionCommand()
            {
                Order = question.Order,
                Text = question.Text,
                A = question.A,
                B = question.B,
                C = question.C,
                D = question.D,
                CorrectAnswer = question.CorrectAnswer,
                Explanation = question.Explanation
            };
        }

        public static EditQuestionCommand ToEditQuestionCommand(this Question question)
        {
            return new EditQuestionCommand()
            {
                Id = question.Id,
                Order = question.Order,
                Text = question.Text,
                A = question.A,
                B = question.B,
                C = question.C,
                D = question.D,
                CorrectAnswer = question.CorrectAnswer,
                Explanation = question.Explanation
            };
        }
    }
}
