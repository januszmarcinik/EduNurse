using System.Security.Principal;
using EduNurse.Api.Shared.Result;

namespace EduNurse.Api.Shared.Query
{
    public interface IQueryDispatcher
    {
        TResult Dispatch<TQuery, TResult>(TQuery query)
            where TQuery : IQuery<TResult>
            where TResult : IResult;

        TResult Dispatch<TQuery, TResult>(TQuery query, IPrincipal user)
            where TQuery : IQuery<TResult>
            where TResult : IResult;
    }
}