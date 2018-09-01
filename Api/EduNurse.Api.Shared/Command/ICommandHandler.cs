using System.Threading.Tasks;

namespace EduNurse.Api.Shared.Command
{
    public interface ICommandHandler<in T> where T : ICommand
    {
        Task<Result> HandleAsync(T command);
    }
}