using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace EduNurse.Exams.Tests.Integration.Extensions
{
    internal static class HttpClientExtensions
    {
        public static ApiResponse<T> GetApiResponse<T>(this Task<HttpResponseMessage> httpResponseMessageTask)
        {
            var httpResponseMessage = httpResponseMessageTask.GetAwaiter().GetResult();
            var result = httpResponseMessage.Content.ReadAsStringAsync().GetAwaiter().GetResult();
            var body = JsonConvert.DeserializeObject<T>(result);
            var apiResponse = new ApiResponse<T>(body, httpResponseMessage.StatusCode);

            return apiResponse;
        }

        public static StringContent ToStringContent(this object body)
        {
            return new StringContent(
                body.ToJson(),
                Encoding.UTF8,
                "application/json"
            );
        }
    }
}
