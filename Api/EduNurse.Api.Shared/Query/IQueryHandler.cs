using EduNurse.Api.Shared.Result;

namespace EduNurse.Api.Shared.Query
{
    public interface IQueryHandler<in TQuery, out TResult>
        where TQuery : IQuery<TResult>
        where TResult : IResult
    {
        TResult Handle(TQuery query);
    }
}