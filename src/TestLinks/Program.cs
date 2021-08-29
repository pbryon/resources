using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using TestLinks.Extensions;
using TestLinks.Model;

namespace TestLinks
{
    internal class Program
    {
        private static readonly HttpClient _client = new HttpClient().Configure();

        private static TestOutput _output;

        private static async Task Main(string[] args)
        {
            args = args.GetLogLevel(out var logLevel);
            _output = new TestOutput(logLevel);

            var topics = GetTopics(args);
            _output.WriteIntro(topics);

            bool hadError = false;
            List<string> broken;
            List<Link> links;
            string name;

            foreach (var topic in topics) {
                broken = new List<string>();
                name = Path.GetFileNameWithoutExtension(topic);

                _output.WriteTopicIntro(name);

                links = await ParseLinks(topic);
                foreach (var link in links) {
                    if (await LinkWorks(link))
                        continue;

                    broken.Add(link.Text);
                    hadError = true;
                }


                _output.WriteTopicStatus(name, broken);
            }

            Environment.Exit(hadError ? 1 : 0);
        }

        private static IEnumerable<string> GetTopics(string[] args)
        {
            string basename;
            var output = new List<string>();
            var search = new List<string>();

            foreach (string arg in args) {
                search.Add(Path.GetFileNameWithoutExtension(arg));
            }

            foreach (var file in Directory.EnumerateFiles(GetTopicDir(), "*.md")) {
                basename = Path.GetFileNameWithoutExtension(file);
                if (args.Length == 0 || search.Any(x => x.ToLower() == basename.ToLower()))
                    output.Add(file);
            }
            return output;
        }

        private static string GetTopicDir()
        {
            string src = AppDomain.CurrentDomain.BaseDirectory;
            while (!src.EndsWith("src")) {
                src = Directory.GetParent(src).ToString();
            }

            var root = Directory.GetParent(src).ToString();
            var topics = Path.Combine(root, "topics");

            if (!Directory.Exists(topics))
                _output.FailWith("Directory not found: {0}", topics);

            return topics;
        }

        private static async Task<List<Link>> ParseLinks(string file)
        {
            var links = new List<Link>();
            string link = @"\[(.+)\]\((.+)\)";
            string anchor = @"#.*$";
            string text, name, url;
            Match match;

            using (var reader = File.OpenText(file)) {
                text = await reader.ReadToEndAsync();
                foreach (string line in text.Split('\n'))
                {
                    if (line.StartsWith("-"))
                        continue;

                    match = Regex.Match(line, link);
                    while (match.Success) {
                        name = match.Groups[1].Value;
                        url = match.Groups[2].Value;
                        match = match.NextMatch();
                        bool hasDomainName = url.Split('.').Length > 1;

                        if (!hasDomainName)
                            continue;

                        if (!url.StartsWith("http"))
                            url = $"http://{url}";

                        if (Regex.IsMatch(url, anchor))
                            url = Regex.Replace(url, anchor, "");

                        links.Add(new Link(name, url));
                    }
                }
            }
            return links;
        }

        private static async Task<bool> LinkWorks(Link link)
        {
            HttpResponseMessage response;
            var error = new Exception();
            try {
                response = await _client.GetAsync(link.Url);
            }
            catch (Exception ex) {
                error = ex;
                response = new HttpResponseMessage(HttpStatusCode.BadRequest) {
                    Content = new StringContent(""),
                    RequestMessage = new HttpRequestMessage(HttpMethod.Get, link.Url)
                };
            }

            _output.ShowLinkStatus(link, response);
            await _output.ShowLinkDebug(response, error);

            return response.IsSuccessStatusCode
                || await response.ContainsJavascriptError();
        }
    }
}
