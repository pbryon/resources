using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace TestLinks
{
    public static class LogLevelExtensions
    {
        public static bool IsQuiet(this LogLevel level) =>
            level == LogLevel.Quiet
            || level == LogLevel.Debug;

        public static bool IsMinimal(this LogLevel level) => level.HasFlag(LogLevel.Minimal);
        public static bool IsVerbose(this LogLevel level) => level.HasFlag(LogLevel.Verbose);
        public static bool IsDebug(this LogLevel level) => level.HasFlag(LogLevel.Debug);

        /// <summary>
        /// Applies the <paramref name="args"/>, returning a filtered argument list.
        /// </summary>
        /// <param name="level"></param>
        /// <param name="args">The command-line arguments</param>
        /// <returns>A filtered list of command-line arguments</returns>
        public static string[] ApplyFlags(this ref LogLevel level, string[] args)
        {
            var output = new List<string>();
            Match match;

            foreach (string arg in args) {
                match = Regex.Match(arg, @"^-{1,2}(.+)$");
                if (!match.Success) {
                    output.Add(arg);
                    continue;
                }

                string flag = match.Groups[1].Value;

                switch (flag) {
                    case "verbose":
                        level |= LogLevel.Verbose;
                        break;
                    case "minimal":
                        level = level & ~LogLevel.Verbose;
                        level |= LogLevel.Minimal;
                        break;
                    case "quiet":
                        level = level & ~LogLevel.Minimal & ~LogLevel.Verbose;
                        break;
                    case "debug":
                        level |= LogLevel.Debug;
                        break;
                }
            }

            return output.ToArray();
        }
    }
}
