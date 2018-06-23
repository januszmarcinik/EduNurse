using System.Collections.Generic;
using Newtonsoft.Json;

namespace EduNurse.Exams.Tests.Integration
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
