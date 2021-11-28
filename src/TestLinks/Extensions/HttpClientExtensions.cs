using System;
using System.Net.Http;

namespace TestLinks.Extensions
{
    public static class HttpClientExtensions
    {
        public static HttpClient Configure(this HttpClient client)
        {
            client.Timeout = TimeSpan.FromMinutes(1);
            client.DefaultRequestHeaders.TryAddWithoutValidation("Accept", "text/html");
            client.DefaultRequestHeaders.TryAddWithoutValidation("User-Agent", "script");

            return client;
        }
    }
}
