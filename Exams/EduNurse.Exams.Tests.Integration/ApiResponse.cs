using System.Net;

namespace EduNurse.Exams.Tests.Integration
{
    internal class ApiResponse
    {
        public ApiResponse(string body, HttpStatusCode statusCode)
        {
            Body = body;
            StatusCode = statusCode;
        }

        public string Body { get; }
        public HttpStatusCode StatusCode { get; }
    }
}
