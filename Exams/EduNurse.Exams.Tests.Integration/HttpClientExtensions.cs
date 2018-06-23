using System.Net.Http;
using System.Threading.Tasks;

namespace EduNurse.Exams.Tests.Integration
{
    internal static class HttpClientExtensions
    {
        public static ApiResponse GetApiResponse(this Task<HttpResponseMessage> httpResponseMessageTask)
        {
            var httpResponseMessage = httpResponseMessageTask.GetAwaiter().GetResult();
            var body = httpResponseMessage.Content.ReadAsStringAsync().GetAwaiter().GetResult();
            var apiResponse = new ApiResponse(body, httpResponseMessage.StatusCode);

            return apiResponse;
        }
    }
}
