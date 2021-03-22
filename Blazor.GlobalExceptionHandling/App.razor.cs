using System;
using System.Threading.Tasks;
using Blazor.GlobalExceptionHandling.Models;
using Blazor.IndexedDB.WebAssembly;
using Microsoft.AspNetCore.Components;
using Microsoft.Extensions.Logging;
using Microsoft.JSInterop;
using MudBlazor;

namespace Blazor.GlobalExceptionHandling
{
    public partial class App
    {
        [Inject] public ISnackbar Snackbar { get; set; }
        [Inject] public IIndexedDbFactory DbFactory { get; set; }

        private static ISnackbar _snackbar;
        private static IIndexedDbFactory _contextFactory;

        protected override async Task OnInitializedAsync()
        {
            _snackbar = Snackbar ?? throw new ArgumentNullException(nameof(Snackbar));
            _contextFactory = DbFactory ?? throw new ArgumentNullException(nameof(DbFactory));
            await base.OnInitializedAsync();
        }


        [JSInvokable("HandleJSException")]
        public static async Task HandleJSException(JavaScriptException error)
        {
            if (!Enum.TryParse(error.LogLevel, true, out LogLevel logLevel))
            {
                logLevel = LogLevel.Error;
            }

            if (logLevel != LogLevel.Critical)
            {
                OpenSnackbar(error.ErrorMessage);
            }

            using var db = await _contextFactory.Create<LoggingDbContext>();
            db.Logs.Add(new LogEntry
            {
                ErrorMessage = error.ErrorMessage,
                Destination = error.LogDestination,
                LogLevel = logLevel,
                Date = DateTime.Now
            });
            await db.SaveChanges();
        }

        private static void OpenSnackbar(string message)
        {
            _snackbar.Add(message, Severity.Error);
        }
    }

    public class JavaScriptException
    {
        public string ErrorMessage { get; set; }
        public string LogLevel { get; set; }
        public string LogDestination { get; set; }
    }
}