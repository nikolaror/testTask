using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MittoAgSMS.Services.Abstractions
{
    public interface ISendSmsService
    {
        Task<bool> SendSMS(MittoAgSMS.DomainModel.Sms domainMessage);
        Task<int> InsertSentSms(MittoAgSMS.DomainModel.Sms message);
    }
}
