using MittoAgSMS.Services.Abstractions;
using Serilog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MittoAgSMS.Services
{
    public class LoggerService : ILoggerService
    {
        public LoggerService()
        {
            Log.Logger = new LoggerConfiguration()
                .MinimumLevel.Debug()
                .WriteTo.File("\\SMS_LOG\\SMS.txt", rollingInterval: RollingInterval.Day)
                .CreateLogger();
        }

        public void LogDebug(string message)
        {
            Log.Logger.Debug(message);
        }

        public void LogError(string message)
        {
            Log.Logger.Error(message);
        }
    }
}
