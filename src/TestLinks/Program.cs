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
        private static HttpClient client = new HttpClient();
        private static bool debug = false;

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

            var topics = GetTopics( args );
            WriteIntro(topics);

            bool hadError = false;
            List<string> broken;
            List<Link> links;
            string name;

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

                STD_ERR.WriteLine( $"--> broken links for '{name}':" );
                foreach( var link in broken ) {
                    STD_ERR.WriteLine( $"  {link}" );
                }
            }

            Environment.Exit( hadError ? 1 : 0 );
        }

        private static void SetupHttpClient()
        {
            client.Timeout = TimeSpan.FromMinutes( 1 );
            client.DefaultRequestHeaders.TryAddWithoutValidation( "Accept", "text/html" );
            client.DefaultRequestHeaders.TryAddWithoutValidation( "User-Agent", "script" );
        }

        private static void WriteIntro( IEnumerable<string> args ) {
            if ( debug )
                Console.Write( "Debug mode ON" );

            if ( args.Count() == 1 )
                return;

            Console.Write( "Checking topic links" );
            bool first = true;
            if ( args.Count() > 0 ) {
                Console.Write( " for topics:");
                foreach( var arg in args ) {
                    Console.Write( "{0} {1}", first ? "" : ",", arg );
                    first = false;
                }
            }
            Console.WriteLine("...");
        }

        private static IEnumerable<string> GetTopics ( string[] args ) {
            string basename;
            var output = new List<string>();
            var search = new List<string>();

            foreach( string arg in args ) {
                if ( Regex.IsMatch( arg, "^-{0,2}debug$" ) )
                    debug = true;
                else
                    search.Add( Path.GetFileNameWithoutExtension(arg) );
            }

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
                STD_ERR.WriteLine( "Directory not found: {0}", topics );
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

            using( var reader = File.OpenText(file)) {
                text = await reader.ReadToEndAsync();
                foreach( string line in text.Split('\n')) {
                    match = Regex.Match( line, link );
                    while ( match.Success ) {
                        name = match.Groups[1].Value;
                        url = match.Groups[2].Value;
                        match = match.NextMatch();
                        hasDomainName = url.Split(".").Length > 1;

                        if ( !url.StartsWith( "http" ) && !hasDomainName )
                            continue;

                        if ( Regex.IsMatch( url, anchor ) )
                            url = Regex.Replace( url, anchor, "" );

                        links.Add( new Link( name, url ) );
                    }
                }
            }
            return links;
        }

        private static async Task<bool> LinkWorks( Link link )
        {
            HttpResponseMessage response;
            string more = "";
            try {
                response = await client.GetAsync( link.Url );
            }
            catch (Exception ex ) {
                if ( ex.InnerException != null )
                    more = ex.InnerException.Message;
                response = new HttpResponseMessage(HttpStatusCode.Ambiguous);
            }

            Console.ForegroundColor = response.IsSuccessStatusCode ? GREEN : RED;
            Console.Write( "  [{0}]", (int) response.StatusCode );
            Console.ForegroundColor = ORIGINAL_COLOR;
            Console.WriteLine( $" {link.Url}" );

            if ( debug && !response.IsSuccessStatusCode ) {
                response.RequestMessage = response.RequestMessage ?? new HttpRequestMessage();
                response.Content = response.Content ?? new ByteArrayContent(new byte[] { });

                Console.ForegroundColor = YELLOW;
                Console.WriteLine( $"--> {response.ReasonPhrase}" );
                Console.WriteLine( $"--> Request:\n'{response.RequestMessage.ToString()}'" );
                Console.WriteLine( $"--> Response:\n{response.ToString()}" );
                string content = await response.Content.ReadAsStringAsync();
                Console.WriteLine( $"--> Content:\n{content}" );
                Console.ForegroundColor = ORIGINAL_COLOR;
            }

            if ( !string.IsNullOrWhiteSpace(more) ) {
                Console.ForegroundColor = YELLOW;
                Console.WriteLine($"  --> {more}");
                Console.ForegroundColor = ORIGINAL_COLOR;
            }

            return response.IsSuccessStatusCode;
        }
    }
}
