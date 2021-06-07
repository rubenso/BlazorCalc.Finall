using System.IO;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace BlazorCalc.Logger
{
    [ProviderAlias("LogFileLoggerProvider")]
    public class LogFileLoggerProvider : ILoggerProvider
    {
        public readonly LogFileLoggerOptions Options;

        public LogFileLoggerProvider(IOptions<LogFileLoggerOptions> options)
        {
            Options = options.Value;

            if (!Directory.Exists(Options.FolderPath)) Directory.CreateDirectory(Options.FolderPath);
        }

        public ILogger CreateLogger(string categoryName)
        {
            return new LogFileLogger(this);
        }

        public void Dispose()
        {
        }
    }
}