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
            Log.Logger = new LoggerConfiguration()
                            .MinimumLevel.Debug()
                            .WriteTo.File("\\sent\\SMS.txt", rollingInterval: RollingInterval.Day)
                            .CreateLogger();
            _sentSmsRepository = sentSmsRepository;
        }

        public bool SendSMS(Sms message)
        {
            try
            {
                Log.Logger.Debug(message.ToString());
            }
            catch(Exception ex)
            {
                return false;
            }
            return true;
        }

        public void InsertSentSms(Sms message)
        {
            _sentSmsRepository.Add(message);
            _sentSmsRepository.Save();
        }
    }
}
