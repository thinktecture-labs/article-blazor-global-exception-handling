using System;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Blazor.GlobalExceptionHandling.Services;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using MudBlazor;
using MudBlazor.Services;

namespace Blazor.GlobalExceptionHandling
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = WebAssemblyHostBuilder.CreateDefault(args);
            builder.RootComponents.Add<App>("#app");
            builder.Services.AddScoped(
                sp => new HttpClient {BaseAddress = new Uri(builder.HostEnvironment.BaseAddress)});
            builder.Services.AddMudServices();
            builder.Services.AddMudBlazorResizeListener();
            builder.Services.AddMudBlazorSnackbar(config =>
            {
                config.PositionClass = Defaults.Classes.Position.TopCenter;
                config.PreventDuplicates = false;
                config.NewestOnTop = false;
                config.ShowCloseIcon = false;
                config.VisibleStateDuration = 1000;
                config.HideTransitionDuration = 500;
                config.ShowTransitionDuration = 500;
            });

            CreateCustomLoggingProvider(builder);

            await builder.Build().RunAsync();
        }

        private static void CreateCustomLoggingProvider(WebAssemblyHostBuilder builder)
        {
            var navigationManager = builder.Services.Single(
                s => s.ServiceType == typeof(NavigationManager)).ImplementationInstance as NavigationManager;
            var customLoggerProvider = new CustomLoggingProvider(navigationManager);
            builder.Logging.AddProvider(customLoggerProvider);
        }
    }
}