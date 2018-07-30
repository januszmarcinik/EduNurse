using System.Security.Principal;

namespace EduNurse.Api.Shared.Query
{
    public interface IQueryDispatcher
    {
        TResult Dispatch<TResult>(IQuery<TResult> query);
        TResult Dispatch<TResult>(IQuery<TResult> query, IPrincipal user);
    }
}