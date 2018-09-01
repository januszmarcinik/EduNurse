using EduNurse.Api.Shared.Query;
using Newtonsoft.Json;

namespace EduNurse.Api.Shared
{
    public class Result
    {
        public bool IsSuccess { get; }
        public string Message { get; }

        [JsonConstructor]
        protected internal Result(bool isSuccess, string message)
        {
            IsSuccess = isSuccess;
            Message = message;
        }

        public static Result Success()
        {
            return new Result(true, string.Empty);
        }

        public static Result<T> Success<T>(T content)
        {
            return new Result<T>(true, string.Empty, content);
        }

        public static Result Failure(string message)
        {
            return new Result(false, message ?? string.Empty);
        }

        public static Result<T> Failure<T>(string message)
        {
            return new Result<T>(false, message, default(T));
        }

        public static Result<T> Failure<T>(IQuery<T> query, string message)
        {
            return new Result<T>(false, message, default(T));
        }
    }

    public class Result<T> : Result
    {
        public T Content { get; }

        [JsonConstructor]
        internal Result(bool isSuccess, string message, T content) : base(isSuccess, message)
        {
            Content = content;
        }
    }
}
