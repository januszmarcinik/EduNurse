using System;
using EduNurse.Api.Shared.Command;
using EduNurse.Exams.Shared.Commands;

namespace EduNurse.Exams.CommandHandlers
{
    internal class DeleteExamCommandHandler : ICommandHandler<DeleteExamCommand>
    {
        private readonly IExamsRepository _examsRepository;
        private readonly Guid _examId;

        public DeleteExamCommandHandler(IExamsRepository examsRepository, Guid examId)
        {
            _examsRepository = examsRepository;
            _examId = examId;
        }

        public void Handle(DeleteExamCommand command)
        {
            var exam = _examsRepository.GetById(_examId);
            if (exam == null)
            {
                throw new NullReferenceException($"Exam with ID '{_examId}' was not found.");
            }

            _examsRepository.Remove(exam);
        }
    }
}
