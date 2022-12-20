using Microsoft.Extensions.Logging;
using MyShop.ApplicationCore.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyShop.Infastructure
{
    public sealed class LoggerAdapter<T> : IAppLogger<T>
    {
        private readonly ILogger<T> _logger;

        public LoggerAdapter(ILoggerFactory logger)
        {
            _logger = logger.CreateLogger<T>();
        }

        public void LogError(Exception exception, string? message, params object[] arg)
        {
            _logger.LogError(exception, message, arg);
        }

        public void LogInformation(string message, params object[] arg)
        {
            _logger.LogInformation(message, arg);
        }

        public void LogWarning(string message, params object[] arg)
        {
            _logger.LogWarning(message, arg);
        }
    }
}
