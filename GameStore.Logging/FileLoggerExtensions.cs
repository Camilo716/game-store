using Microsoft.Extensions.Logging;

namespace GameStore.Logging;

public static class FileLoggerExtensions
{
    public static ILoggerFactory AddFileLoggingProvider(this ILoggerFactory factory, string filePath)
    {
        factory.AddProvider(new FileLoggerProvider(filePath));
        return factory;
    }
}