using System;
using System.Threading.Tasks;
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

        public async Task HandleAsync(DeleteExamCommand command)
        {
            var exam = await _examsRepository.GetByIdAsync(_examId);
            if (exam == null)
            {
                throw new NullReferenceException($"Exam with ID '{_examId}' was not found.");
            }

            await _examsRepository.RemoveAsync(exam);
        }
    }
}
