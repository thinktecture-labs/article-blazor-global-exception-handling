using System;
using Microsoft.AspNetCore.Components;
using Microsoft.Extensions.Logging;
using Microsoft.JSInterop;
using MudBlazor;

namespace Blazor.GlobalExceptionHandling
{
    public partial class App
    {
        [Inject] public ISnackbar Snackbar { get; set; }
        [Inject] public ILoggerProvider LoggerProvider { get; set; }

        private static ISnackbar _snackbar;
        private static ILogger _logger;

        protected override void OnInitialized()
        {
            _snackbar = Snackbar ?? throw new ArgumentException(nameof(Snackbar));
            _logger = LoggerProvider.CreateLogger(nameof(App));
            base.OnInitialized();
        }


        [JSInvokable("HandleJSException")]
        public static void HandleJSException(string error)
        {
            _logger.Log(LogLevel.Error, 0, error);
            OpenDialog(error);
        }

        private static void OpenDialog(string message)
        {
            _snackbar.Add(message, Severity.Error);
        }
    }
}