using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;
using TestLinks.Extensions;
using TestLinks.Model;

namespace TestLinks
{
    public class TopicValidator
    {
        private readonly TestOutput _output;
        private readonly HttpClient _client;
        private readonly Regex IsLink = new(@"\[(.+)\]\((.+)\)", RegexOptions.Compiled | RegexOptions.CultureInvariant);
        private readonly Regex IsAnchor = new(@"#.*$", RegexOptions.Compiled | RegexOptions.CultureInvariant);

        public TopicValidator(LogLevel logLevel)
        {
            _output = new TestOutput(logLevel);
            _client = new HttpClient().Configure();
        }

        public async Task<bool> Validate(CancellationToken cancellationToken, params string[] args)
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

                links = await ParseLinks(topic, cancellationToken);
                foreach (var link in links)
                {
                    if (await LinkWorks(link, cancellationToken))
                        continue;

                    broken.Add(link);
                    hadError = true;
                }


                _output.WriteTopicStatus(name, broken);
            }

            return hadError;
        }

        private static IEnumerable<string> GetTopics(string[] args)
        {
            string basename;
            var search = new List<string>();

            foreach (string arg in args)
            {
                search.Add(Path.GetFileNameWithoutExtension(arg));
            }

            foreach (var file in Directory.EnumerateFiles(GetTopicDir(), "*.md"))
            {
                basename = Path.GetFileNameWithoutExtension(file);
                if (args.Length == 0 || search.Any(x => x.ToLower() == basename.ToLower()))
                    yield return file;
            }
        }

        private static string GetTopicDir()
        {
            string src = AppDomain.CurrentDomain.BaseDirectory;
            while (!src.EndsWith("src"))
            {
                src = Directory.GetParent(src).ToString();
            }

            var root = Directory.GetParent(src).ToString();
            var topics = Path.Combine(root, "topics");

            if (!Directory.Exists(topics))
                TestOutput.FailWith("Directory not found: {0}", topics);

            return topics;
        }

        private async Task<List<Link>> ParseLinks(string file, CancellationToken cancellationToken)
            => (await File.ReadAllTextAsync(file, cancellationToken))
                .Split('\n')
                .SelectMany(line => CreateLinks(line))
                .ToList();

        private IEnumerable<Link> CreateLinks(string line)
        {
            if (line.StartsWith("-"))
                yield break;

            var match = IsLink.Match(line);
            while (match.Success)
            {
                var (name, url) = (match.Groups[1].Value, match.Groups[2].Value);
                match = match.NextMatch();
                bool hasDomainName = url.Split('.').Length > 1;

                if (!hasDomainName)
                    continue;

                if (!url.StartsWith("http"))
                    url = $"http://{url}";

                if (IsAnchor.IsMatch(url))
                    url = IsAnchor.Replace(url, "");

                yield return new Link(name, url);
            }
        }

        private async Task<bool> LinkWorks(Link link, CancellationToken cancellationToken)
        {
            HttpResponseMessage response;
            var error = new Exception();
            try
            {
                response = await _client.GetAsync(link.Url, cancellationToken);
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

            link = await GetLinkDetails(link, response, error, cancellationToken);
            
            _output.ShowLinkStatus(link);
            _output.ShowLinkDebug(link);

            return response.IsSuccessStatusCode
                || link.HasJavascriptError;
        }

        private static async Task<Link> GetLinkDetails(Link link, HttpResponseMessage response, Exception ex, CancellationToken cancellationToken)
        {
            link = link with
            {
                IsValidated = response.IsSuccessStatusCode,
                StatusCode = response.StatusCode
            };

            if (response.IsSuccessStatusCode)
                return link;

            var content = await response.GetResponseBodyText(cancellationToken);

            if (content.Length > 500)
                content = $"{content.Substring(0, 500)} [...]";

            string message = ex.InnerException?.Message ?? ex.Message;
            response.RequestMessage ??= new HttpRequestMessage();

            return link with
            {
                HasJavascriptError = content.ContainsJavascriptError(),
                Content = content,
                Request = response.RequestMessage.ToString(),
                Response = response.ToString(),
                Message = message
            };
        }
    }
}
