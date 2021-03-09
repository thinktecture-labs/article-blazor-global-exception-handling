using System;
using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using MudBlazor;

namespace Blazor.GlobalExceptionHandling
{
    public partial class App
    {
        [Inject] public ISnackbar Snackbar { get; set; }

        private static ISnackbar _snackbar;

        protected override void OnInitialized()
        {
            _snackbar = Snackbar ?? throw new ArgumentException(nameof(Snackbar));
            base.OnInitialized();
        }


        [JSInvokable("HandleJSException")]
        public static void HandleJSException(string error)
        {
            OpenDialog(error);
        }

        private static void OpenDialog(string message)
        {
            _snackbar.Add(message, Severity.Error);
        }

        private static bool TryGetMessageFromJsError(object error, out string message)
        {
            message = String.Empty;
            var t = error.GetType();
            foreach (var propertyInfo in t.GetProperties())
            {
                Console.WriteLine($"Property: {propertyInfo.Name}");
            }

            var prop = t.GetProperty("message");
            if (prop != null)
            {
                Console.WriteLine("Has property message");
                var value = prop.GetValue(error);
                if (value != null)
                {
                    message = value.ToString();
                }
            }

            return false;
        }
    }
}