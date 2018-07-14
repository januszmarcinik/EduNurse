using EduNurse.Exams.Shared;
using Microsoft.AspNetCore.Mvc;

namespace EduNurse.Exams.Api.Commands
{
    public interface ICommandHandler<in T> where T : ICommand
    {
        IActionResult Handle(T command);
    }
}
