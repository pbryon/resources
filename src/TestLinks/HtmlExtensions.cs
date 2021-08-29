using HtmlAgilityPack;
using System.Collections.Generic;
using System.Linq;

namespace TestLinks
{
    public static class HtmlExtensions
    {
        private static readonly string[] LinkTypes = new[] { "a", "link" };
        private static readonly string[] FormControls = new[] { "form", "select", "button", "input", "i", "img" };
        private static readonly string[] Headers = new[] { 1..7 }.Select(x => $"h{x}").ToArray();

        public static HtmlNode? GetBody(this HtmlDocument document)
            => document.DocumentNode
                .ChildNodes
                .FirstOrDefault(x => x.Name == "html")
                ?.ChildNodes
                .FirstOrDefault(x => x.Name == "body");

        public static IEnumerable<HtmlNode> Filter(this IEnumerable<HtmlNode> nodes)
        {
            if (!nodes.Any())
                yield break;

            var filtered =
                nodes
                    .WithoutScripts()
                    .WithoutLinks()
                    .WithoutForms()
                    .WithoutComments()
                    .WithoutHeaders()
                    .WithoutWhitespace();
            
            foreach(var node in filtered)
            {
                if (node.ChildNodes.Any())
                {
                    var relevant = node.ChildNodes.Filter().ToList();
                    foreach (var child in relevant)
                        yield return child;
                }
                else
                {
                    if (!string.IsNullOrWhiteSpace(node.InnerText))
                        yield return node;
                }
            }
        }

        public static IEnumerable<HtmlNode> WithoutScripts(this IEnumerable<HtmlNode> nodes)
            => nodes.Where(x => x.Name != "script");

        public static IEnumerable<HtmlNode> WithoutLinks(this IEnumerable<HtmlNode> nodes)
            => nodes.Where(x => !LinkTypes.Contains(x.Name));

        public static IEnumerable<HtmlNode> WithoutForms(this IEnumerable<HtmlNode> nodes)
            => nodes.Where(x => !FormControls.Contains(x.Name));

        public static IEnumerable<HtmlNode> WithoutHeaders(this IEnumerable<HtmlNode> nodes)
            => nodes.Where(x => !Headers.Contains(x.Name));

        public static IEnumerable<HtmlNode> WithoutComments(this IEnumerable<HtmlNode> nodes)
            => nodes.Where(x => x.NodeType != HtmlNodeType.Comment);

        public static IEnumerable<HtmlNode> WithoutWhitespace(this IEnumerable<HtmlNode> nodes)
        {
            var result = nodes.Where(x =>
                        {
                            var trimmed = x.InnerText.Trim();
                            return !string.IsNullOrWhiteSpace(trimmed);
                        }).ToList();

            return result.AsEnumerable();
        }
    }
}
