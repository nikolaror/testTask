using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MittoAgSMS.DomainModel;

namespace MittoAgSMS.Services.Abstractions
{
    public interface IGetSentSmsService
    {
        Sms[] GetSentSms(SentSmsFilterRequest dlRequest);
    }
}
