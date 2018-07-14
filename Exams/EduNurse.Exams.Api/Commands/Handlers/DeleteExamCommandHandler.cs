using System;
using EduNurse.Exams.Shared.Commands;
using Microsoft.AspNetCore.Mvc;

namespace EduNurse.Exams.Api.Commands.Handlers
{
    internal class DeleteExamCommandHandler : ICommandHandler<DeleteExamCommand>
    {
        private readonly ExamsContext _context;
        private readonly Guid _examId;

        public DeleteExamCommandHandler(ExamsContext context, Guid examId)
        {
            _context = context;
            _examId = examId;
        }

        public IActionResult Handle(DeleteExamCommand command)
        {
            var exam = _context.Exams.Find(_examId);
            if (exam == null)
            {
                throw new NullReferenceException($"Exam with ID '{_examId}' was not found.");
            }

            _context.Exams.Remove(exam);
            _context.SaveChanges();

            return new AcceptedResult();
        }
    }
}
