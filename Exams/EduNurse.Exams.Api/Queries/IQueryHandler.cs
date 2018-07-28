using EduNurse.Exams.Shared;

namespace EduNurse.Exams.Api.Queries
{
    public interface IQueryHandler<in TQuery, out TResult> 
        where TQuery : IQuery<TResult>
        where TResult : IResult
    {
        TResult Handle(TQuery query);
    }
}
