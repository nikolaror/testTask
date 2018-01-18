using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MittoAgSMS.Services.Abstractions
{
    public interface ISendSmsService
    {
        bool SendSMS(MittoAgSMS.DomainModel.Sms domainMessage);
        void InsertSentSms(MittoAgSMS.DomainModel.Sms message);
    }
}
