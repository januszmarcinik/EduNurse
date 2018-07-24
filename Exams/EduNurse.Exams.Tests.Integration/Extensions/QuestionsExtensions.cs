using EduNurse.Exams.Api.Entities;
using EduNurse.Exams.Shared.Commands;
using EduNurse.Exams.Shared.Results;

namespace EduNurse.Exams.Tests.Integration.Extensions
{
    internal static class QuestionsExtensions
    {
        public static QuestionResult ToQuestionResult(this Question question)
        {
            return new QuestionResult()
            {
                Id = question.Id,
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
