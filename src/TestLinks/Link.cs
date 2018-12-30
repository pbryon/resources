namespace TestLinks
{
    internal class Link
    {
        public string Text { get; private set; }
        public string Url { get; private set; }

        public Link(string text, string url)
        {
            Text = text;
            Url = url;
        }
    }
}
