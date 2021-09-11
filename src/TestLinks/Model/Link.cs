using System.Net;

namespace TestLinks.Model
{
    internal record Link(string Topic, string Text, string Url)
    {
        public string Content { get; set; }
        public bool HasJavascriptError { get; set; }
        public string Request { get; set; }
        public string Response { get; set; }
        public string Message { get; set; }
        public bool IsValidated { get; set; }
        public HttpStatusCode StatusCode { get; set; }
    }
}
