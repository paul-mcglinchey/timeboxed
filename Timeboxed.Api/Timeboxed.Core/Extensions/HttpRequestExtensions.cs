using Microsoft.AspNetCore.Http;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Newtonsoft.Json;

namespace Timeboxed.Core.Extensions
{
    public static class HttpRequestExtensions
    {
        internal const string AuthCookieName = "UserAuth";

        public static bool TryGetAuthCookie(this HttpRequest httpRequest, out string cookie) => httpRequest.Cookies.TryGetValue(AuthCookieName, out cookie);

        public static bool TryGetAuthHeader(this HttpRequest httpRequest, out string authHeader) {
            httpRequest.Headers.TryGetValue("authorization", out var authHeaders);

            if (authHeaders.ToString().Split(",").Length == 0)
            {
                authHeader = null;
                return false;
            }

            authHeader = new List<string>(authHeaders.ToString().Split(",")).Find(header => header.Contains("Bearer"));

            if (authHeader != null && authHeader.Split(" ").Length == 2)
            {
                authHeader = authHeader.Split(" ")[1];
            }
            else
            {
                authHeader = null;
            }

            return authHeader is not null;
        }

        public static async Task<T> ConstructRequestModelAsync<T>(this HttpRequest req)
        {
            var content = await new StreamReader(req.Body).ReadToEndAsync();

            return JsonConvert.DeserializeObject<T>(content);
        }

        public static T DeserializeQueryParams<T>(this HttpRequest req)
        {
            try
            {
                var json = JsonConvert.SerializeObject(req.GetQueryParameterDictionary());
                return JsonConvert.DeserializeObject<T>(json);
            }
            catch
            {
                return default(T);
            }
        }
    }
}
