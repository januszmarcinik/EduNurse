using System.Net;

namespace EduNurse.Exams.Tests.Integration
{
    internal class ApiResponse<T>
    {
        public ApiResponse(T body, HttpStatusCode statusCode)
        {
            Body = body;
            StatusCode = statusCode;
        }

        public T Body { get; }
        public HttpStatusCode StatusCode { get; }
    }
}
