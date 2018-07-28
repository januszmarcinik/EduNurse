using EduNurse.Exams.Shared;

namespace EduNurse.Exams.Api.Commands
{
    public interface ICommandHandler<in T> where T : ICommand
    {
        void Handle(T command);
    }
}
