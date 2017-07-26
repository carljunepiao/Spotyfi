using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Extensions.Logging;

namespace SpotyfiLib.SpotyfiLib.Infrastructure.Cross_Cutting.LoggingFac
{
    public class FedLogger
    {
        public static ILoggerFactory LoggerFactory { get; set; }
        public static ILogger CreateLogger<T>() => LoggerFactory.CreateLogger<T>();
    }
}
