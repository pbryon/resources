namespace TestLinks.Model
{
    internal record Link(string Text, string Url)
    {
        public string Content { get; set; }
        public bool HasJavascriptError { get; set; }
    }
}
