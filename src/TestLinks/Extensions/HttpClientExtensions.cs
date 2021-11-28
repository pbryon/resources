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
            client.DefaultRequestHeaders.TryAddWithoutValidation("User-Agent", "Mozilla/5.0 (X11; Linux x86_64; rv:10.0) Gecko/20100101 Firefox/94.0");

            return client;
        }
    }
}
