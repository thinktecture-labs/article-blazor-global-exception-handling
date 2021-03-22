using System;
using Microsoft.AspNetCore.Components;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.Abstractions;

namespace Blazor.GlobalExceptionHandling.Services
{
    public class CirticalExceptionLoggingProvider : ILoggerProvider
    {
        private CriticalExceptionLogger _logger;

        public CirticalExceptionLoggingProvider(NavigationManager navigationManager)
        {
            _logger = new CriticalExceptionLogger(navigationManager);
        }

        public void Dispose()
        {
            _logger = null;
        }

        public ILogger CreateLogger(string categoryName)
        {
            if (_logger != null)
            {
                return _logger;
            }

            return NullLogger.Instance;
        }
    }

    public class CriticalExceptionLogger : ILogger
    {
        private readonly NavigationManager _navigationManager;

        public CriticalExceptionLogger(NavigationManager navigationManager)
        {
            _navigationManager = navigationManager ?? throw new ArgumentNullException(nameof(navigationManager));
        }

        public void Log<TState>(LogLevel logLevel, EventId eventId, TState state, Exception exception,
            Func<TState, Exception, string> formatter)
        {
            // To continue using the UI the page must be reloaded after an unhandled exception with the LogLevel Critical was thrown
            if (logLevel == LogLevel.Critical)
            {
                _navigationManager.NavigateTo(_navigationManager.Uri, true);
            }
        }

        public bool IsEnabled(LogLevel logLevel)
        {
            return logLevel == LogLevel.Error;
        }

        public IDisposable BeginScope<TState>(TState state)
        {
            return null;
        }
    }
}