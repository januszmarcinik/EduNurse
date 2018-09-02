using System.Collections.Generic;
using Newtonsoft.Json;

namespace EduNurse.Api.Tests.Integration.Extensions
{
    public static class JsonExtensions
    {
        public static string ToJson(this IEnumerable<object> @object)
        {
            return JsonConvert.SerializeObject(@object);
        }

        public static string ToJson(this object @object)
        {
            return JsonConvert.SerializeObject(@object);
        }
    }
}
