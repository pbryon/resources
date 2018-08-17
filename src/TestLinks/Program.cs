using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace TestLinks
{
    class Program
    {
        static async Task Main(string[] args)
        {
            WriteIntro(args);

            bool hadError = false;
            List<string> broken;
            List<Link> links;
            string name;

            var topics = GetTopics(args);
            TextWriter stderr = Console.Error;

            foreach( var topic in topics ) {
                broken = new List<string>();
                name = Path.GetFileNameWithoutExtension( topic );
                Console.WriteLine( $"\nTopic '{name}':" );
                links = await ParseLinks( topic );
                foreach( var link in links ) {
                    if ( await LinkWorks( link ) )
                        continue;

                    broken.Add( link.Text );
                    hadError = true;
                }

                if ( broken.Count == 0 ) {
                    Console.WriteLine( "--> topic OK" );
                    continue;
                }

                stderr.WriteLine( $"--> broken links for '{name}':" );
                foreach( var link in broken ) {
                    stderr.WriteLine( $"  {link}" );
                }
            }

            Environment.Exit( hadError ? 1 : 0 );
        }

        private static void WriteIntro( string[] args ) {
            if ( args.Length == 1 )
                return;
            Console.Write( "Checking topic links" );
            bool first = true;
            if ( args.Length > 0 ) {
                Console.Write( " for topics:");
                foreach( var arg in args ) {
                    Console.Write( "{0} {1}", first ? "" : ",", arg );
                    first = false;
                }
            }
            Console.WriteLine("...");
        }

        private static IEnumerable<string> GetTopics ( string[] args ) {
            var output = new List<string>();
            var search = new List<string>();
            foreach( string arg in args ) {
                search.Add( Path.GetFileNameWithoutExtension(arg) );
            }
            string basename;
            foreach ( var file in Directory.EnumerateFiles( GetTopicDir(), "*.md" ) ) {
                basename = Path.GetFileNameWithoutExtension(file);
                if ( args.Length == 0 || search.Any(x => x.ToLower() == basename.ToLower() ) )
                    output.Add(file);
            }
            return output;
        }

        private static string GetTopicDir()
        {
            string src = AppDomain.CurrentDomain.BaseDirectory;
            while ( !src.EndsWith( "src" ) ) {
                src = Directory.GetParent( src ).ToString();
            }

            var root = Directory.GetParent( src ).ToString();
            var topics = Path.Combine( root, "topics" );

            if ( !Directory.Exists( topics ) ) {
                Console.WriteLine( "Directory not found: {0}", topics );
                Environment.Exit(1);
            }
            return topics;
        }

        private static async Task<List<Link>> ParseLinks(string file)
        {
            var links = new List<Link>();
            string link = @"\[(.+)\]\((.+)\)";
            string text, name, url;
            Match match;

            using( var reader = File.OpenText(file)) {
                text = await reader.ReadToEndAsync();
                foreach( string line in text.Split('\n')) {
                    match = Regex.Match( line, link );
                    while ( match.Success ) {
                        name = match.Groups[1].Value;
                        url = match.Groups[2].Value;
                        match = match.NextMatch();

                        if ( !url.StartsWith( "http" ) )
                            continue;

                        links.Add( new Link( name, url ) );
                    }
                }
            }
            return links;
        }

        private static async Task<bool> LinkWorks( Link link )
        {
            var original = Console.ForegroundColor;

            using ( var client = new HttpClient() ) {
                client.Timeout = new TimeSpan(0,0,10);

                HttpResponseMessage response;
                string more = "";
                try {
                    client.DefaultRequestHeaders.TryAddWithoutValidation("Accept", "text/html");
                    client.DefaultRequestHeaders.TryAddWithoutValidation("User-Agent", "script");
                    client.Timeout = new TimeSpan(0, 0, 30);
                    response = await client.GetAsync( link.Url );
                }
                catch (Exception ex ) {
                    if ( ex.InnerException != null )
                        more = ex.InnerException.Message;
                    response = new HttpResponseMessage(HttpStatusCode.Ambiguous);
                }

                Console.ForegroundColor = response.IsSuccessStatusCode ? ConsoleColor.Green : ConsoleColor.Red;
                Console.Write( "  [{0}]", (int) response.StatusCode );
                Console.ForegroundColor = original;
                Console.WriteLine( $" {link.Url}" );
                if ( !string.IsNullOrWhiteSpace(more) ) {
                    Console.ForegroundColor = ConsoleColor.Yellow;
                    Console.WriteLine($"  --> {more}");
                    Console.ForegroundColor = original;
                }

                return response.IsSuccessStatusCode;
            }
        }
    }
}
