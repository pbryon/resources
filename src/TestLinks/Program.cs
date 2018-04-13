using System;
using System.Collections.Generic;
using System.IO;
using System.Net.Http;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace TestLinks
{
    class Program
    {
        static async Task Main(string[] args)
        {
            Console.WriteLine( "Checking topic links..." );

            var dir = GetTopicDir();
            bool hadError = false;
            List<string> broken;
            List<Link> links;
            string topicName;
            foreach( var topic in Directory.EnumerateFiles(dir, "*.md" )) {
                broken = new List<string>();
                topicName = Path.GetFileNameWithoutExtension( topic );
                Console.WriteLine( "\nTopic '{0}':", topicName );
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

                Console.WriteLine( "--> broken links:" );
                foreach( var link in broken ) {
                    Console.WriteLine( $"{link}" );
                }
            }

            Environment.Exit( hadError ? 1 : 0 );
        }

        private static void SetupConsole ()
        {
            var stdout = Console.OpenStandardOutput();
            var con = new StreamWriter( stdout, Encoding.ASCII );
            con.AutoFlush = true;
            Console.SetOut( con );
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
                var response = await client.GetAsync( link.Url );

                Console.ForegroundColor = response.IsSuccessStatusCode ? ConsoleColor.Green : ConsoleColor.Red;
                Console.Write( "  [{0}]", (int) response.StatusCode );
                Console.ForegroundColor = original;
                Console.WriteLine( $" {link.Url}" );

                return response.IsSuccessStatusCode;
            }
        }
    }
}
