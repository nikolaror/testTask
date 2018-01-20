using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MittoAgSMS.Services.Abstractions
{
    public interface ILoggerService
    {
        void LogDebug(string message);
        void LogError(string message);
    }
}
