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
    public class TopicValidator
    {
        private TestOutput _output;
        private readonly HttpClient _client;
        private readonly Regex IsLink = new(@"\[(.+)\]\((.+)\)", RegexOptions.Compiled | RegexOptions.CultureInvariant);
        private readonly Regex IsAnchor = new(@"#.*$", RegexOptions.Compiled | RegexOptions.CultureInvariant);

        public TopicValidator(LogLevel logLevel)
        {
            _output = new TestOutput(logLevel);
            _client = new HttpClient().Configure();
        }

        public async Task<bool> Validate(params string[] args)
        {
            var topics = GetTopics(args);
            _output.WriteIntro(topics);

            bool hadError = false;
            List<Link> broken;
            List<Link> links;
            string name;

            foreach (var topic in topics)
            {
                broken = new List<Link>();
                name = Path.GetFileNameWithoutExtension(topic);

                _output.WriteTopicIntro(name);

                links = await ParseLinks(topic);
                foreach (var link in links)
                {
                    if (await LinkWorks(link))
                        continue;

                    broken.Add(link);
                    hadError = true;
                }


                _output.WriteTopicStatus(name, broken);
            }

            return hadError;
        }

        private IEnumerable<string> GetTopics(string[] args)
        {
            string basename;
            var output = new List<string>();
            var search = new List<string>();

            foreach (string arg in args)
            {
                search.Add(Path.GetFileNameWithoutExtension(arg));
            }

            foreach (var file in Directory.EnumerateFiles(GetTopicDir(), "*.md"))
            {
                basename = Path.GetFileNameWithoutExtension(file);
                if (args.Length == 0 || search.Any(x => x.ToLower() == basename.ToLower()))
                    output.Add(file);
            }
            return output;
        }

        private string GetTopicDir()
        {
            string src = AppDomain.CurrentDomain.BaseDirectory;
            while (!src.EndsWith("src"))
            {
                src = Directory.GetParent(src).ToString();
            }

            var root = Directory.GetParent(src).ToString();
            var topics = Path.Combine(root, "topics");

            if (!Directory.Exists(topics))
                _output.FailWith("Directory not found: {0}", topics);

            return topics;
        }

        private async Task<List<Link>> ParseLinks(string file)
        {
            var links = new List<Link>();
            using var reader = File.OpenText(file);
            var text = await reader.ReadToEndAsync();

            foreach (string line in text.Split('\n'))
            {
                if (line.StartsWith("-"))
                    continue;

                var match = IsLink.Match(line);
                links.AddRange(CreateLinks(match));
            }

            return links.ToList();
        }

        private IEnumerable<Link> CreateLinks(Match match)
        {
            while (match.Success)
            {
                var (name, url) = (match.Groups[1].Value, match.Groups[2].Value);
                bool hasDomainName = url.Split('.').Length > 1;

                if (!hasDomainName)
                    continue;

                if (!url.StartsWith("http"))
                    url = $"http://{url}";

                if (IsAnchor.IsMatch(url))
                    url = IsAnchor.Replace(url, "");

                yield return new Link(name, url);
                match.NextMatch();
            }
        }

        private async Task<bool> LinkWorks(Link link)
        {
            HttpResponseMessage response;
            var error = new Exception();
            try
            {
                response = await _client.GetAsync(link.Url);
            }
            catch (Exception ex)
            {
                error = ex;
                response = new HttpResponseMessage(HttpStatusCode.BadRequest)
                {
                    Content = new StringContent(""),
                    RequestMessage = new HttpRequestMessage(HttpMethod.Get, link.Url)
                };
            }

            _output.ShowLinkStatus(link, response);
            await _output.ShowLinkDebug(link, response, error);

            link = link with { 
                HasJavascriptError = await response.ContainsJavascriptError()
            };

            return response.IsSuccessStatusCode
                || link.HasJavascriptError;
        }
    }
}
