using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using EduNurse.Api.Shared;
using Newtonsoft.Json;

namespace EduNurse.Exams.Tests.Integration.Extensions
{
    internal static class HttpClientExtensions
    {
        public static Result GetResult(this Task<HttpResponseMessage> httpResponseMessageTask)
        {
            var httpResponseMessage = httpResponseMessageTask.GetAwaiter().GetResult();
            var result = httpResponseMessage.Content.ReadAsStringAsync().GetAwaiter().GetResult();
            return JsonConvert.DeserializeObject<Result>(result);
        }

        public static Result<T> GetResult<T>(this Task<HttpResponseMessage> httpResponseMessageTask)
        {
            var httpResponseMessage = httpResponseMessageTask.GetAwaiter().GetResult();
            var result = httpResponseMessage.Content.ReadAsStringAsync().GetAwaiter().GetResult();
            return JsonConvert.DeserializeObject<Result<T>>(result);
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
