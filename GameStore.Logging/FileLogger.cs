using Microsoft.Extensions.Logging;

namespace GameStore.Logging;

public class FileLogger(string path) : ILogger
{
    private static readonly object _lock = new();

    public IDisposable? BeginScope<TState>(TState state)
        where TState : notnull
    {
        return null;
    }

    public bool IsEnabled(LogLevel logLevel)
    {
        return true;
    }

    public void Log<TState>(
        LogLevel logLevel,
        EventId eventId,
        TState state,
        Exception? exception,
        Func<TState, Exception?, string> formatter)
    {
        if (formatter is null)
        {
            return;
        }

        lock (_lock)
        {
            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            }

            string fullFilePath = Path.Combine(path, $"{DateTime.Now:yyyy-MM-dd}_log.txt");
            string exc = string.Empty;
            var n = Environment.NewLine;

            if (exception != null)
            {
                exc = $"{n}{exception.GetType()}:{exception.Message}{n}{exception.StackTrace}{n}";
            }

            string log = $"{logLevel}: {DateTime.Now} {formatter(state, exception)}{n}{exc}{n}";
            File.AppendAllText(fullFilePath, log);
        }
    }
}
