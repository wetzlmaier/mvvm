using Microsoft.Extensions.Logging;
using Serilog;
using Serilog.Core;
using Serilog.Events;
using Serilog.Extensions.Logging;
using System.ComponentModel.Composition;

namespace Logging
{
    [Export(typeof(ILoggingService))]
    public class LoggingService : ILoggingService
    {
        private readonly Microsoft.Extensions.Logging.ILogger _logger = new SerilogLoggerFactory(
                new LoggerConfiguration()
                    .Enrich.WithThreadId()
                    .Enrich.WithThreadName()
                    .Enrich.With(new CategoryNameEnricher(string.Empty))
                    .MinimumLevel.Debug()
                    .WriteTo.File(path: "Hargassner.Teststand-.log", rollingInterval: RollingInterval.Day,
                        retainedFileCountLimit: 5)
                    .WriteTo.Debug(outputTemplate: "{Timestamp:HH:mm:ss} [{Level}] [{CategoryName}] [{ThreadId}] " +
                                                   "[{ThreadName}] {Message}{NewLine}{Exception}")
                    .CreateLogger())
            .CreateLogger(string.Empty);

        public void Log<TState>(LogLevel logLevel, EventId eventId, TState state, Exception? exception, Func<TState, Exception?, string> formatter)
        {
            _logger.Log(logLevel, eventId, state, exception, formatter);
        }

        public bool IsEnabled(LogLevel logLevel)
        {
            return _logger.IsEnabled(logLevel);
        }

        public IDisposable? BeginScope<TState>(TState state) where TState : notnull
        {
            return _logger.BeginScope(state);
        }

        class CategoryNameEnricher(string categoryName) : ILogEventEnricher
        {
            private readonly string _categoryName = categoryName;

            public void Enrich(LogEvent logEvent, ILogEventPropertyFactory propertyFactory)
            {
                logEvent.AddPropertyIfAbsent(propertyFactory.CreateProperty("CategoryName", _categoryName));
            }
        }

        public  void LogDebug( EventId eventId, Exception? exception, string? message, params object?[] args)
        {
            _logger.Log(LogLevel.Debug, eventId, exception, message, args);
        }

        public  void LogDebug( EventId eventId, string? message, params object?[] args)
        {
            _logger.Log(LogLevel.Debug, eventId, message, args);
        }

        public  void LogDebug( Exception? exception, string? message, params object?[] args)
        {
            _logger.Log(LogLevel.Debug, exception, message, args);
        }

        public  void LogDebug( string? message, params object?[] args)
        {
            _logger.Log(LogLevel.Debug, message, args);
        }

        public  void LogTrace( EventId eventId, Exception? exception, string? message, params object?[] args)
        {
            _logger.Log(LogLevel.Trace, eventId, exception, message, args);
        }

        public  void LogTrace( EventId eventId, string? message, params object?[] args)
        {
            _logger.Log(LogLevel.Trace, eventId, message, args);
        }
        public  void LogTrace( Exception? exception, string? message, params object?[] args)
        {
            _logger.Log(LogLevel.Trace, exception, message, args);
        }
        public  void LogTrace( string? message, params object?[] args)
        {
            _logger.Log(LogLevel.Trace, message, args);
        }

        public  void LogInformation( EventId eventId, Exception? exception, string? message, params object?[] args)
        {
            _logger.Log(LogLevel.Information, eventId, exception, message, args);
        }

        public  void LogInformation( EventId eventId, string? message, params object?[] args)
        {
            _logger.Log(LogLevel.Information, eventId, message, args);
        }

        public  void LogInformation( Exception? exception, string? message, params object?[] args)
        {
            _logger.Log(LogLevel.Information, exception, message, args);
        }

        public  void LogInformation( string? message, params object?[] args)
        {
            _logger.Log(LogLevel.Information, message, args);
        }

        public  void LogWarning( EventId eventId, Exception? exception, string? message, params object?[] args)
        {
            _logger.Log(LogLevel.Warning, eventId, exception, message, args);
        }

        public  void LogWarning( EventId eventId, string? message, params object?[] args)
        {
            _logger.Log(LogLevel.Warning, eventId, message, args);
        }

        public  void LogWarning( Exception? exception, string? message, params object?[] args)
        {
            _logger.Log(LogLevel.Warning, exception, message, args);
        }

        public  void LogWarning( string? message, params object?[] args)
        {
            _logger.Log(LogLevel.Warning, message, args);
        }

        public  void LogError( EventId eventId, Exception? exception, string? message, params object?[] args)
        {
            _logger.Log(LogLevel.Error, eventId, exception, message, args);
        }

        public  void LogError( EventId eventId, string? message, params object?[] args)
        {
            _logger.Log(LogLevel.Error, eventId, message, args);
        }

        public  void LogError( Exception? exception, string? message, params object?[] args)
        {
            _logger.Log(LogLevel.Error, exception, message, args);
        }

        public  void LogError( string? message, params object?[] args)
        {
            _logger.Log(LogLevel.Error, message, args);
        }

        public  void LogCritical( EventId eventId, Exception? exception, string? message, params object?[] args)
        {
            _logger.Log(LogLevel.Critical, eventId, exception, message, args);
        }

        public  void LogCritical( EventId eventId, string? message, params object?[] args)
        {
            _logger.Log(LogLevel.Critical, eventId, message, args);
        }

        public  void LogCritical( Exception? exception, string? message, params object?[] args)
        {
            _logger.Log(LogLevel.Critical, exception, message, args);
        }

        public  void LogCritical( string? message, params object?[] args)
        {
            _logger.Log(LogLevel.Critical, message, args);
        }

        public  void Log( LogLevel logLevel, string? message, params object?[] args)
        {
            _logger.Log(logLevel, 0, null, message, args);
        }

        public  void Log( LogLevel logLevel, EventId eventId, string? message, params object?[] args)
        {
            _logger.Log(logLevel, eventId, null, message, args);
        }

        public  void Log( LogLevel logLevel, Exception? exception, string? message, params object?[] args)
        {
            _logger.Log(logLevel, 0, exception, message, args);
        }
    }
}
