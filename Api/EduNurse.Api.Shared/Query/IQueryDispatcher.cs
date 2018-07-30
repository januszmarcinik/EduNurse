using System.Security.Principal;
using EduNurse.Api.Shared.Result;

namespace EduNurse.Api.Shared.Query
{
    public interface IQueryDispatcher
    {
        TResult Dispatch<TResult>(IQuery<TResult> query) where TResult : IResult;
        TResult Dispatch<TResult>(IQuery<TResult> query, IPrincipal user) where TResult : IResult;
    }
}