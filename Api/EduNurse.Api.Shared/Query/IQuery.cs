using EduNurse.Api.Shared.Result;

namespace EduNurse.Api.Shared.Query
{
    public interface IQuery<TResult> where TResult : IResult
    {
    }
}