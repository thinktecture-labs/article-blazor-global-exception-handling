using System;
using System.Collections.Specialized;
using System.Threading.Tasks;
using Blazor.GlobalExceptionHandling.Services;
using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;

namespace Blazor.GlobalExceptionHandling.Pages
{
    public partial class Index
    {
        [Inject] public IJSRuntime _jsRuntime { get; set; }

        protected override async Task OnInitializedAsync()
        {
            await base.OnInitializedAsync();
        }

        private void ThrowUnhandledException()
        {
            throw new Exception("Unhandled .NET Core Exception is thrown");
        }

        private async Task ThrowJSException()
        {
            await _jsRuntime.InvokeVoidAsync("generateRejectedPromise");
        }
    }
}