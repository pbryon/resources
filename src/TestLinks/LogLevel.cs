using System;

namespace TestLinks
{
    [Flags]
    public enum LogLevel
    {
        Quiet = 0,
        Minimal = 1,
        Verbose = 2,
        Debug = 4
    }
}
