using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using MudBlazor;

namespace Blazor.GlobalExceptionHandling
{
    public partial class App
    {
        [Inject] public ISnackbar Snackbar { get; set; }

        private static ISnackbar _snackbar;

        protected override async Task OnInitializedAsync()
        {
            _snackbar = Snackbar ?? throw new ArgumentException(nameof(Snackbar));
            await base.OnInitializedAsync();
        }


        [JSInvokable("HandleJSException")]
        public static void HandleJSException(string error)
        {
            Console.WriteLine($"Add exception to handling. {error}");
            OpenSnackbar(error);
        }

        private static void OpenSnackbar(string message)
        {
            _snackbar.Add(message, Severity.Error);
        }
    }
}