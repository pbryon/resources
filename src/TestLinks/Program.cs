using System;
using System.Threading.Tasks;
using TestLinks.Extensions;

namespace TestLinks
{
    internal class Program
    {
        private static async Task Main(string[] args)
        {
            args = args.GetLogLevel(out var logLevel);
            var hadError = await new TopicValidator(logLevel).Validate(args);

            Environment.Exit(hadError ? 1 : 0);
        }
    }
}
