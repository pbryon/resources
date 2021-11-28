using System;
using System.Threading;
using TestLinks;
using TestLinks.Extensions;

args = args.GetLogLevel(out var logLevel);
var hadError =
    await new TopicValidator(logLevel)
    .Validate(new CancellationTokenSource().Token, args);

Environment.Exit(hadError ? 1 : 0);