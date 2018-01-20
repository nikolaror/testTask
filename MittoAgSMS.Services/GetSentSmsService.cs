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

        public async Task<Sms[]> GetSentSms(SentSmsFilterRequest dlRequest)
        {
            return await _sentSmsRepository.FilterSentSms(dlRequest);
        }
    }
}
