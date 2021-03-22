using Blazor.IndexedDB.WebAssembly;
using Microsoft.JSInterop;

namespace Blazor.GlobalExceptionHandling.Models
{
    public class LoggingDbContext : IndexedDb
    {
        public LoggingDbContext(IJSRuntime jSRuntime, string name, int version)
            : base(jSRuntime, name, version)
        {
        }

        public IndexedSet<LogEntry> Logs { get; set; }
    }
}