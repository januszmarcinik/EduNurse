using System;
using System.Security.Principal;
using EduNurse.Exams.Shared;
using Microsoft.AspNetCore.Mvc;

namespace EduNurse.Exams.Api.Commands
{
    public interface ICommandDispatcher
    {
        IActionResult Dispatch<T>(T command) where T : ICommand;
        IActionResult Dispatch<T>(T command, IPrincipal user) where T : ICommand;
        IActionResult Dispatch<T>(T command, Guid objectId) where T : ICommand;
        IActionResult Dispatch<T>(T command, IPrincipal user, Guid objectId) where T : ICommand;
    }
}
