using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using TestLinks.Extensions;
using TestLinks.Model;

namespace TestLinks
{
    internal class TestOutput
    {
        private readonly LogLevel _logLevel;
        
        /// <summary>
        /// <see cref="Console.Error"/>
        /// </summary>
        private static readonly TextWriter STD_ERR = Console.Error;
        /// <summary>
        /// The original <see cref="Console.ForegroundColor"/> at startup.
        /// </summary>
        private static readonly ConsoleColor ORIGINAL_COLOR = Console.ForegroundColor;

        public TestOutput(LogLevel logLevel)
        {
            _logLevel = logLevel;
        }

        public void WriteIntro(IEnumerable<string> args)
        {
            if (!_logLevel.IsVerbose())
                return;

            if (args.Count() == 1)
                return;

            Console.Write("Checking topic links");

            bool first = true;
            if (args.Any())
            {
                Console.Write(" for topics:");

                foreach (var arg in args)
                {
                    Console.Write("{0} {1}", first ? "" : ",", Path.GetFileNameWithoutExtension(arg));
                    first = false;
                }
            }

            Console.WriteLine("...");
        }

        public void WriteTopicIntro(string topic)
        {
            if (_logLevel.IsQuiet())
                return;

            string message = $"Topic '{topic}'";

            if (_logLevel.IsMinimal())
            {
                Console.Write($"{message}..");
                return;
            }

            Console.WriteLine();
            Console.WriteLine($"{message}:");
        }

        public void FailWith(string reason, params object[] args)
        {
            STD_ERR.WriteLine(reason, args);
            Environment.Exit(1);
        }

        public void ShowLinkStatus(Link link, HttpResponseMessage response)
        {
            if (_logLevel.IsMinimal())
                Console.Write(".");

            if (response.IsSuccessStatusCode && !_logLevel.IsVerbose())
                return;

            Console.ForegroundColor = response.IsSuccessStatusCode
                ? ConsoleColor.Green
                : ConsoleColor.Red;
            
            Console.Write("  [{0}]", (int)response.StatusCode);
            Console.ForegroundColor = ORIGINAL_COLOR;
            Console.WriteLine($" {link.Url}");
        }

        public async Task ShowLinkDebug(HttpResponseMessage response, Exception ex)
        {
            if (response.IsSuccessStatusCode)
                return;

            if (!_logLevel.IsDebug())
                return;

            response.RequestMessage ??= new HttpRequestMessage();

            Console.ForegroundColor = ConsoleColor.Yellow;
            string indent = " ";
            WritePadded(indent, response.RequestMessage.ToString(), "Request");
            WritePadded(indent, response.ToString(), "Response");

            string content = await response.GetResponseBodyText();

            if (content?.Length > 500)
                content = $"{content.Substring(0, 500)} [...]";

            WritePadded(indent, content, "Content");
            Console.ForegroundColor = ORIGINAL_COLOR;

            string message = ex.InnerException?.Message ?? ex.Message;
            if (!string.IsNullOrWhiteSpace(message))
            {
                Console.ForegroundColor = ConsoleColor.Yellow;
                WritePadded(indent, message, "Exception");
                Console.ForegroundColor = ORIGINAL_COLOR;
            }
        }

        private static void WritePadded(string prefix, string text, string header = null)
        {
            Console.WriteLine($"{prefix} {header}:");
            string indent = new string(' ', prefix.Length + 3);
            string[] lines = text
                .Replace(", ", ",\n")
                .Split('\n');

            foreach (string line in lines)
            {
                Console.WriteLine($"{indent}{line}");
            }
        }

        public void WriteTopicStatus(string topic, List<string> brokenLinks)
        {
            if (_logLevel.IsQuiet())
                return;

            if (brokenLinks.Count > 0)
            {
                if (_logLevel.IsQuiet())
                    Console.WriteLine();
                STD_ERR.WriteLine($"--> broken links for '{topic}':");

                foreach (var link in brokenLinks)
                {
                    STD_ERR.WriteLine($"  {link}");
                }

                return;
            }

            if (_logLevel.IsMinimal())
            {
                Console.WriteLine(" OK");
                return;
            }

            Console.WriteLine("--> topic OK");
        }
    }
}
