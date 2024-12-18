using Microsoft.Extensions.Logging;

namespace GameStore.Logging;

public class FileLoggerProvider(string path) : ILoggerProvider
{
    private readonly string _path = path;

    public ILogger CreateLogger(string categoryName)
    {
        return new FileLogger(_path);
    }

    public void Dispose()
    {
        GC.SuppressFinalize(this);
    }
}