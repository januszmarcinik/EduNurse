using System.Security.Principal;
using EduNurse.Exams.Shared;
using Microsoft.AspNetCore.Mvc;

namespace EduNurse.Exams.Api.Queries
{
    public interface IQueryDispatcher
    {
        IActionResult Dispatch<TQuery>(TQuery query) where TQuery : IQuery;

        IActionResult Dispatch<TQuery>(TQuery query, IPrincipal user) where TQuery : IQuery;
    }
}
