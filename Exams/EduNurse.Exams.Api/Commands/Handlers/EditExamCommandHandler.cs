using System;
using System.Linq;
using EduNurse.Exams.Shared.Commands;
using Microsoft.EntityFrameworkCore;

namespace EduNurse.Exams.Api.Commands.Handlers
{
    internal class EditExamCommandHandler : ICommandHandler<EditExamCommand>
    {
        private readonly ExamsContext _context;
        private readonly Guid _examId;

        public EditExamCommandHandler(ExamsContext context, Guid examId)
        {
            _context = context;
            _examId = examId;
        }

        public void Handle(EditExamCommand command)
        {
            var exam = _context.Exams
                .Include(p => p.Questions)
                .SingleOrDefault(x => x.Id == _examId);

            if (exam == null)
            {
                throw new NullReferenceException($"Exam with ID '{_examId}' was not found.");
            }

            exam.SetName(command.Name);
            exam.ChangeType(command.Type);
            exam.SetCategory(command.Category);

            var commandQuestionIds = command.Questions
                .Where(x => x.Id.HasValue)
                .Select(p => p.Id.Value)
                .ToList();

            exam.Questions
                .Select(x => x.Id)
                .Except(commandQuestionIds)
                .ToList()
                .ForEach(id => exam.RemoveQuestion(id));

            foreach (var q in command.Questions)
            {
                if (q.Id.HasValue)
                {
                    var existedQuestion = exam.Questions.SingleOrDefault(x => x.Id == q.Id);
                    if (existedQuestion != null)
                    {
                        existedQuestion.SetOrder(q.Order);
                        existedQuestion.SetText(q.Text);
                        existedQuestion.SetAnswers(q.A, q.B, q.C, q.D);
                        existedQuestion.SetExplantation(q.Explanation);
                        existedQuestion.ChangeCorrectAnswer(q.CorrectAnswer);
                        continue;
                    }
                }

                exam.AddQuestion(q.Order, q.Text, q.A, q.B, q.C, q.D, q.CorrectAnswer, q.Explanation);
            }

            _context.Update(exam);
            _context.SaveChanges();
        }
    }
}
