using System;
using System.Security.Principal;
using EduNurse.Exams.Shared;

namespace EduNurse.Exams.Api.Commands
{
    public interface ICommandDispatcher
    {
        void Dispatch<T>(T command) where T : ICommand;
        void Dispatch<T>(T command, IPrincipal user) where T : ICommand;
        void Dispatch<T>(T command, Guid objectId) where T : ICommand;
        void Dispatch<T>(T command, IPrincipal user, Guid objectId) where T : ICommand;
    }
}
