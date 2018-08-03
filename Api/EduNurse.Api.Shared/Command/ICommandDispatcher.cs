using System;
using System.Security.Principal;
using System.Threading.Tasks;

namespace EduNurse.Api.Shared.Command
{
    public interface ICommandDispatcher
    {
        Task DispatchAsync<T>(T command) where T : ICommand;
        Task DispatchAsync<T>(T command, IPrincipal user) where T : ICommand;
        Task DispatchAsync<T>(T command, Guid objectId) where T : ICommand;
        Task DispatchAsync<T>(T command, IPrincipal user, Guid objectId) where T : ICommand;
    }
}