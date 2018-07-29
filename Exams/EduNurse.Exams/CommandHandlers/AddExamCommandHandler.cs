using System;
using System.Linq;
using System.Security.Principal;
using EduNurse.Api.Shared.Command;
using EduNurse.Exams.Entities;
using EduNurse.Exams.Shared.Commands;

namespace EduNurse.Exams.CommandHandlers
{
    internal class AddExamCommandHandler : ICommandHandler<AddExamCommand>
    {
        private readonly ExamsContext _context;
        private readonly IPrincipal _user;

        public AddExamCommandHandler(ExamsContext context, IPrincipal user)
        {
            _context = context;
            _user = user;
        }

        public void Handle(AddExamCommand command)
        {
            var examId = Guid.NewGuid();
            var exam = new Exam(examId, command.Name, command.Type, command.Category, _user.Identity.Name, DateTime.Now, false);
            _context.Exams.Add(exam);

            var questions = command.Questions
                .Select(q => new Question(Guid.NewGuid(), examId, q.Order, q.Text, q.A, q.B, q.C, q.D, q.CorrectAnswer, q.Explanation));
            _context.Questions.AddRange(questions);

            _context.SaveChanges();
        }
    }
}
