using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace TestLinks
{
    class Program
    {
        private static HttpClient client = new HttpClient();
        private static LogLevel logLevel = LogLevel.Verbose;

        /// <summary>
        /// <see cref="Console.Error"/>
        /// </summary>
        private static readonly TextWriter STD_ERR = Console.Error;
        /// <summary>
        /// The original <see cref="Console.ForegroundColor"/> at startup.
        /// </summary>
        private static readonly ConsoleColor ORIGINAL_COLOR = Console.ForegroundColor;
        /// <summary>
        /// <see cref="ConsoleColor.Red"/>
        /// </summary>
        private static readonly ConsoleColor RED = ConsoleColor.Red;
        /// <summary>
        /// <see cref="ConsoleColor.Yellow"/>
        /// </summary>
        private static readonly ConsoleColor YELLOW = ConsoleColor.Yellow;
        /// <summary>
        /// <see cref="ConsoleColor.Green"/>
        /// </summary>
        private static readonly ConsoleColor GREEN = ConsoleColor.Green;

        static async Task Main(string[] args)
        {
            SetupHttpClient();
            args = logLevel.ApplyFlags(args);

            var topics = GetTopics(args);
            WriteIntro(topics);

            bool hadError = false;
            List<string> broken;
            List<Link> links;
            string name;

            foreach ( var topic in topics ) {
                broken = new List<string>();
                name = Path.GetFileNameWithoutExtension(topic);

                if (!logLevel.IsQuiet())
                    Console.WriteLine($"\nTopic '{name}':");

                links = await ParseLinks(topic);
                foreach ( var link in links ) {
                    if ( await LinkWorks(link) )
                        continue;

                    broken.Add(link.Text);
                    hadError = true;
                }

                if ( broken.Count == 0 ) {
                    if ( !logLevel.IsQuiet() )
                        Console.WriteLine("--> topic OK");
                    continue;
                }

                STD_ERR.WriteLine($"--> broken links for '{name}':");
                foreach ( var link in broken ) {
                    STD_ERR.WriteLine($"  {link}");
                }
            }

            Environment.Exit(hadError ? 1 : 0);
        }

        private static void SetupHttpClient()
        {
            client.Timeout = TimeSpan.FromMinutes(1);
            client.DefaultRequestHeaders.TryAddWithoutValidation("Accept", "text/html");
            client.DefaultRequestHeaders.TryAddWithoutValidation("User-Agent", "script");
        }

        private static void WriteIntro(IEnumerable<string> args)
        {
            if ( !logLevel.IsVerbose() )
                return;

            if ( args.Count() == 1 )
                return;

            Console.Write("Checking topic links");

            bool first = true;
            if ( args.Count() > 0 ) {
                Console.Write(" for topics:");

                foreach ( var arg in args ) {
                    Console.Write("{0} {1}", first ? "" : ",", Path.GetFileNameWithoutExtension(arg) );
                    first = false;
                }
            }

            Console.WriteLine("...");
        }

        private static IEnumerable<string> GetTopics(string[] args)
        {
            string basename;
            var output = new List<string>();
            var search = new List<string>();

            foreach ( string arg in args ) {
                search.Add(Path.GetFileNameWithoutExtension(arg));
            }

            foreach ( var file in Directory.EnumerateFiles(GetTopicDir(), "*.md") ) {
                basename = Path.GetFileNameWithoutExtension(file);
                if ( args.Length == 0 || search.Any(x => x.ToLower() == basename.ToLower()) )
                    output.Add(file);
            }
            return output;
        }

        private static string GetTopicDir()
        {
            string src = AppDomain.CurrentDomain.BaseDirectory;
            while ( !src.EndsWith("src") ) {
                src = Directory.GetParent(src).ToString();
            }

            var root = Directory.GetParent(src).ToString();
            var topics = Path.Combine(root, "topics");

            if ( !Directory.Exists(topics) ) {
                STD_ERR.WriteLine("Directory not found: {0}", topics);
                Environment.Exit(1);
            }
            return topics;
        }

        private static async Task<List<Link>> ParseLinks(string file)
        {
            var links = new List<Link>();
            string link = @"\[(.+)\]\((.+)\)";
            string anchor = @"#.*$";
            string text, name, url;
            bool hasDomainName = false;
            Match match;

            using ( var reader = File.OpenText(file) ) {
                text = await reader.ReadToEndAsync();
                foreach ( string line in text.Split('\n') ) {
                    match = Regex.Match(line, link);
                    while ( match.Success ) {
                        name = match.Groups[1].Value;
                        url = match.Groups[2].Value;
                        match = match.NextMatch();
                        hasDomainName = url.Split('.').Length > 1;

                        if ( !hasDomainName )
                            continue;

                        if ( !url.StartsWith("http") )
                            url = $"http://{url}";

                        if ( Regex.IsMatch(url, anchor) )
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
                response = await client.GetAsync(link.Url);
            }
            catch ( Exception ex ) {
                error = ex;
                response = new HttpResponseMessage(HttpStatusCode.BadRequest) {
                    Content = new StringContent(""),
                    RequestMessage = new HttpRequestMessage(HttpMethod.Get, link.Url)
                };
            }

            ShowLinkStatus(link, response);
            await ShowLinkDebug(link, response, error);

            return response.IsSuccessStatusCode;
        }

        private static void ShowLinkStatus(Link link, HttpResponseMessage response)
        {
            if ( response.IsSuccessStatusCode && !logLevel.IsVerbose() )
                return;

            Console.ForegroundColor = response.IsSuccessStatusCode ? GREEN : RED;
            Console.Write("  [{0}]", (int) response.StatusCode);
            Console.ForegroundColor = ORIGINAL_COLOR;
            Console.WriteLine($" {link.Url}");
        }

        private static async Task ShowLinkDebug(Link link, HttpResponseMessage response, Exception ex)
        {
            if ( response.IsSuccessStatusCode )
                return;

            if ( !logLevel.IsDebug() )
                return;

            response.RequestMessage = response.RequestMessage ?? new HttpRequestMessage();

            Console.ForegroundColor = YELLOW;
            string indent = " ";
            WritePadded(indent, response.RequestMessage.ToString(), "Request");
            WritePadded(indent, response.ToString(), "Response");

            string content = "";
            if ( response.Content != null)
                content = await response.Content.ReadAsStringAsync();

            WritePadded(indent, content, "Content" );
            Console.ForegroundColor = ORIGINAL_COLOR;

            string message = ex.InnerException?.Message ?? ex.Message;
            if ( !string.IsNullOrWhiteSpace(message) ) {
                Console.ForegroundColor = YELLOW;
                WritePadded(indent, message, "Exception");
                Console.ForegroundColor = ORIGINAL_COLOR;
            }
        }

        private static void WritePadded( string prefix, string text, string header = null )
        {
            Console.WriteLine($"{prefix} {header}:");
            string indent = new string(' ', prefix.Length + 3);
            string[] lines = text
                .Replace(", ", ",\n")
                .Split('\n');

            foreach ( string line in  lines ) {
                Console.WriteLine($"{indent}{line}");
            }
        }
    }
}
