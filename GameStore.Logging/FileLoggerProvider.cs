using Microsoft.Extensions.Logging;

namespace GameStore.Logging;

public class FileLoggerProvider(string path) : ILoggerProvider
{
    public ILogger CreateLogger(string categoryName)
    {
        return new FileLogger(path);
    }

    public void Dispose()
    {
        Dispose(true);
        GC.SuppressFinalize(this);
    }

    protected virtual void Dispose(bool disposing)
    {
    }
}