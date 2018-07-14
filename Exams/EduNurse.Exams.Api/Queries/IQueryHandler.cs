using EduNurse.Exams.Shared;
using Microsoft.AspNetCore.Mvc;

namespace EduNurse.Exams.Api.Queries
{
    public interface IQueryHandler<in TQuery> where TQuery : IQuery 
    {
        IActionResult Handle(TQuery query);
    }
}
