using System.Security.Principal;
using System.Threading.Tasks;

namespace EduNurse.Api.Shared.Query
{
    public interface IQueryDispatcher
    {
        Task<Result<TResult>> DispatchAsync<TResult>(IQuery<TResult> query);
        Task<Result<TResult>> DispatchAsync<TResult>(IQuery<TResult> query, IPrincipal user);
    }
}