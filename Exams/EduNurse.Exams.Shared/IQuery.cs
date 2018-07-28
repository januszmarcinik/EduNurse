using System.Collections.Generic;

namespace EduNurse.Exams.Shared
{
    public interface IQuery<TResult> where TResult : IResult
    {
    }
}
