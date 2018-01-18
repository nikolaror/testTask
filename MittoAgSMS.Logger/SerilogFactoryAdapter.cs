using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Serilog;
using System.IO;
using MittoAgSMS.Logger.Abstractions;

namespace MittoAgSMS.Logger
{
    public class SerilogFactoryAdapter : ILoggerFactoryAdapter
    {
        public SerilogFactoryAdapter()
        {
            Log.Logger = new LoggerConfiguration()
                            .MinimumLevel.Debug()
                            .WriteTo.File("\\sent\\SMS.txt", rollingInterval: RollingInterval.Day)
                            .CreateLogger();
        }

        public bool SendSMS(string message)
        {
            Log.Logger.Debug(message);
            return true;
        }
    }
}
