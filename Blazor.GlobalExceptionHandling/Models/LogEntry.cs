using System;
using Microsoft.Extensions.Logging;

namespace Blazor.GlobalExceptionHandling.Models
{
    public class LogEntry
    {
        public Guid Id { get; set; }
        public string ErrorMessage { get; set; }
        public ErrorType ErrorType { get; set; }
        public LogLevel LogLevel { get; set; }

        public LogEntry()
        {
            Id = new Guid();
        }
    }

    public enum ErrorType
    {
        JavaScript,
        DotNet
    }
}