using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using TestLinks.Extensions;
using TestLinks.Model;

namespace TestLinks
{
    internal class TestOutput
    {
        private readonly LogLevel _logLevel;
        
        /// <summary>
        /// The original <see cref="Console.ForegroundColor"/> at startup.
        /// </summary>
        private readonly ConsoleColor ORIGINAL_COLOR = Console.ForegroundColor;

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

        public static void FailWith(string reason, params object[] args)
        {
            Console.Error.WriteLine(reason, args);
            Environment.Exit(1);
        }

        public void ShowLinkStatus(Link link)
        {
            if (_logLevel.IsMinimal())
                Console.Write(".");

            if (link.IsValidated && !_logLevel.IsVerbose())
                return;

            var color = link.IsValidated
                ? ConsoleColor.Green
                : ConsoleColor.Red;
            WriteColor(color, () =>
                Console.Write("  [{0}]", (int)link.StatusCode));
            
            Console.WriteLine($" {link.Url}");
        }

        public void ShowLinkDebug(Link link)
        {
            if (link.IsValidated)
                return;

            if (!_logLevel.IsDebug())
                return;

            string indent = " ";

            WriteColor(ConsoleColor.Yellow, () =>
            {
                WritePadded(indent, link.Request, "Request");
                WritePadded(indent, link.Response, "Response");
                WritePadded(indent, link.Content, "Content");
            });

            
            if (!string.IsNullOrWhiteSpace(link.Message))
            {
                WriteColor(ConsoleColor.Yellow, () => WritePadded(indent, link.Message, "Exception"));
            }

            if (link.HasJavascriptError)
            {
                WriteColor(ConsoleColor.Blue, () => Console.WriteLine("> ignored link with Javascript error"));
            }
        }

        private void WriteColor(ConsoleColor color, Action action)
        {
            Console.ForegroundColor = color;
            action?.Invoke();
            Console.ForegroundColor = ORIGINAL_COLOR;
        }

        private static void WritePadded(string prefix, string text, string header = null)
        {
            Console.WriteLine($"{prefix} {header}:");
            string indent = new(' ', prefix.Length + 3);
            string[] lines = text
                .Replace(", ", ",\n")
                .Split('\n');

            foreach (string line in lines)
            {
                Console.WriteLine($"{indent}{line}");
            }
        }

        public void WriteTopicStatus(string topic, List<Link> brokenLinks)
        {
            if (_logLevel.IsQuiet())
                return;

            if (brokenLinks.Count > 0)
            {
                if (_logLevel.IsQuiet())
                    Console.WriteLine();

                Console.Error.WriteLine($"--> broken links for '{topic}':");

                foreach (var link in brokenLinks)
                {
                    Console.Error.WriteLine($"  {link.Text}");
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

        public void WriteResult(List<Link> brokenLinks)
        {
            if (!_logLevel.IsDebug())
                return;

            var ordered = brokenLinks
                .GroupBy(x => x.Topic)
                .OrderBy(x => x);

            foreach(var topic in ordered)
            {
                WriteTopicStatus(topic.Key, topic.ToList());
            }
        }
    }
}
