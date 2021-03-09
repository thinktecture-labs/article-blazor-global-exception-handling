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
    }
}