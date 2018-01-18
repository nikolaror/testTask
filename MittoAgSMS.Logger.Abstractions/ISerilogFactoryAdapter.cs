using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MittoAgSMS.Logger.Abstractions
{
    public interface ILoggerFactoryAdapter
    {
        bool SendSMS(string message);
 }
}
