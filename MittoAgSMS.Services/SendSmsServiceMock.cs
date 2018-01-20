using MittoAgSMS.Services.Abstractions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MittoAgSMS.DomainModel;
using Serilog;
using System.IO;
using MittoAgSMS.DataAccessLayer.Abstraction;

namespace MittoAgSMS.Services
{
    public class SendSmsServiceMock : ISendSmsService
    {
        private readonly ISentSmsRepository _sentSmsRepository;

        public SendSmsServiceMock(ISentSmsRepository sentSmsRepository)
        {
            if (Log.Logger == null)
            {
                Log.Logger = new LoggerConfiguration()
                               .MinimumLevel.Debug()
                               .WriteTo.File("\\sent\\SMS.txt", rollingInterval: RollingInterval.Day)
                               .CreateLogger();
            }
            _sentSmsRepository = sentSmsRepository;
        }

        public async Task<bool> SendSMS(Sms message)
        {
            try
            {
                Log.Logger.Debug(message.ToString());
            }
            catch (Exception ex)
            {
                return await Task.FromResult<bool>(false);
            }
            return await Task.FromResult<bool>(true);
        }

        public async Task<int> InsertSentSms(Sms message)
        {
            _sentSmsRepository.Add(message);
            var result = await _sentSmsRepository.Save();
            return result;
        }
    }
}
