using System;
using System.Security.Principal;

namespace EduNurse.Api.Shared.Command
{
    public interface ICommandDispatcher
    {
        void Dispatch<T>(T command) where T : ICommand;
        void Dispatch<T>(T command, IPrincipal user) where T : ICommand;
        void Dispatch<T>(T command, Guid objectId) where T : ICommand;
        void Dispatch<T>(T command, IPrincipal user, Guid objectId) where T : ICommand;
    }
}