using System;
using Microsoft.AspNetCore.Components;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.Abstractions;

namespace Blazor.GlobalExceptionHandling.Services
{
    public class CustomLoggingProvider : ILoggerProvider
    {
        private CustomLogger _logger;

        public CustomLoggingProvider(NavigationManager navigationManager)
        {
            _logger = new CustomLogger(navigationManager);
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

    public class CustomLogger : ILogger
    {
        private readonly NavigationManager _navigationManager;

        public CustomLogger(NavigationManager navigationManager)
        {
            _navigationManager = navigationManager ?? throw new ArgumentNullException(nameof(navigationManager));
        }

        public void Log<TState>(LogLevel logLevel, EventId eventId, TState state, Exception exception,
            Func<TState, Exception, string> formatter)
        {
            if (logLevel == LogLevel.Critical)
            {
                // _navigationManager.NavigateTo(_navigationManager.BaseUri, true);
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