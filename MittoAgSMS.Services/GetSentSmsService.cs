using MittoAgSMS.DataAccessLayer.Abstraction;
using MittoAgSMS.DomainModel;
using MittoAgSMS.Services.Abstractions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MittoAgSMS.Services
{
    public class GetSentSmsService: IGetSentSmsService
    {
        private readonly ISentSmsRepository _sentSmsRepository;

        public GetSentSmsService(ISentSmsRepository sentSmsRepository)
        {
            _sentSmsRepository = sentSmsRepository;
        }

        public Sms[] GetSentSms(SentSmsFilterRequest dlRequest)
        {
            Sms[] result = _sentSmsRepository.FilterSentSms(dlRequest);
            return result;
        }
    }
}
