﻿using System;
using System.Linq;
using System.Threading.Tasks;
using EduNurse.Api.Shared;
using EduNurse.Api.Shared.Command;
using EduNurse.Exams.Shared.Commands;

namespace EduNurse.Exams.CommandHandlers
{
    internal class EditExamCommandHandler : ICommandHandler<EditExamCommand>
    {
        private readonly IExamsRepository _examsRepository;
        private readonly Guid _examId;

        public EditExamCommandHandler(IExamsRepository examsRepository, Guid examId)
        {
            _examsRepository = examsRepository;
            _examId = examId;
        }

        public async Task<Result> HandleAsync(EditExamCommand command)
        {
            var exam = await _examsRepository.GetByIdAsync(_examId);
            if (exam == null)
            {
                return Result.Failure($"Exam with ID '{_examId}' was not found.");
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

            await _examsRepository.UpdateAsync(exam);

            return Result.Success();
        }
    }
}
