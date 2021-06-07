using System;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace BlazorCalc.Logger
{
    public static class LogFileLogggerExtensions
    {
        public static ILoggingBuilder AddLogFileFileLogger(this ILoggingBuilder builder,
            Action<LogFileLoggerOptions> configure)
        {
            builder.Services.AddSingleton<ILoggerProvider, LogFileLoggerProvider>();
            builder.Services.Configure(configure);
            return builder;
        }
    }
}