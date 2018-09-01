using System.Threading.Tasks;

namespace EduNurse.Api.Shared.Query
{
    public interface IQueryHandler<in TQuery, TResult> where TQuery : IQuery<TResult>
    {
        Task<Result<TResult>> HandleAsync(TQuery query);
    }
}