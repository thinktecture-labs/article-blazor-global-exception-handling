using System;
using Microsoft.Extensions.Logging;

namespace Blazor.GlobalExceptionHandling.Models
{
    public class LogEntry
    {
        public Guid Id { get; set; }
        public string ErrorMessage { get; set; }
        public string Destination { get; set; }
        public LogLevel LogLevel { get; set; }
        public DateTime Date { get; set; }

        public LogEntry()
        {
            Id = Guid.NewGuid();
        }
    }
}