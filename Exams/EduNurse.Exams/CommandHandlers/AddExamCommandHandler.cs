﻿using System;
using System.Security.Principal;
using System.Threading.Tasks;
using EduNurse.Api.Shared;
using EduNurse.Api.Shared.Command;
using EduNurse.Exams.Entities;
using EduNurse.Exams.Shared.Commands;

namespace EduNurse.Exams.CommandHandlers
{
    internal class AddExamCommandHandler : ICommandHandler<AddExamCommand>
    {
        private readonly IExamsRepository _examsRepository;
        private readonly IPrincipal _user;

        public AddExamCommandHandler(IExamsRepository examsRepository, IPrincipal user)
        {
            _examsRepository = examsRepository;
            _user = user;
        }

        public async Task<Result> HandleAsync(AddExamCommand command)
        {
            var exam = new Exam(Guid.NewGuid(), command.Name, command.Type, command.Category, _user.Identity.Name, DateTime.Now, false);

            foreach (var q in command.Questions)
            {
                exam.AddQuestion(q.Order, q.Text, q.A, q.B, q.C, q.D, q.CorrectAnswer, q.Explanation);
            }

            await _examsRepository.AddAsync(exam);

            return Result.Success();
        }
    }
}
