using AutoMapper;
using MittoAgSMS.BusinessLogic.Abstractions;
using MittoAgSMS.BusinessLogic.Validators;
using MittoAgSMS.BusinessModel;
using MittoAgSMS.DomainModel;
using MittoAgSMS.Services.Abstractions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
[assembly: InternalsVisibleTo("MittoAgSMS.Tests")]
namespace MittoAgSMS.BusinessLogic
{
    public class SendSmsBusinessLogic : ISendSmsBusinessLogic
    {
        private readonly ISendSmsService _sendSmsService;
        private readonly IGetSentSmsService _getSentSmsService;
        private readonly ICountriesService _countryService;

        public SendSmsBusinessLogic(ISendSmsService sendSmsService, IGetSentSmsService getSentSmsService, ICountriesService countryService)
        {
            _sendSmsService = sendSmsService;
            _getSentSmsService = getSentSmsService;
            _countryService = countryService;
        }

        public SentSmsFilterResponse GetSentSms(BusinessModel.SentSmsFilterRequest request)
        {
            MittoAgSMS.DomainModel.SentSmsFilterRequest dlRequest = Mapper.Map<MittoAgSMS.BusinessModel.SentSmsFilterRequest, MittoAgSMS.DomainModel.SentSmsFilterRequest>(request);
            DomainModel.Sms[] domainResult = _getSentSmsService.GetSentSms(dlRequest);
            MittoAgSMS.BusinessModel.SentSmsResponse[] itemsBl = Mapper.Map<MittoAgSMS.DomainModel.Sms[], MittoAgSMS.BusinessModel.SentSmsResponse[]>(domainResult);
            return new SentSmsFilterResponse()
            {
                TotalCount = domainResult.Count(),
                Items = itemsBl
            };
        }

        public State SendSMS(BusinessModel.SmsToSend message)
        {
            var phoneNumberValidator = new PhoneNumberValidator();
            phoneNumberValidator.SetModel(message?.To);
            if (!phoneNumberValidator.Validate())
            {
                throw new Exception("Receiver not valid!");
            }

            string countryCode = GetCountryCodeFromNumber(message.To);
            if(string.IsNullOrEmpty(countryCode))
            {
                throw new Exception("Receiver not valid!");
            }
            string MccForPhone = _countryService.GetMccForNumber(countryCode);
            if (string.IsNullOrEmpty(MccForPhone))
            {
                throw new Exception("Mcc not valid!");
            }

            DomainModel.Sms domainMessage = new DomainModel.Sms() { From = message.From, To = message.To, Text = message.Text ?? string.Empty, MobileCountryCode = MccForPhone.Trim(), Sent = DateTime.Now };
            DomainModel.Sms[] messagesList = CreateChunks(domainMessage);
            //
            bool sentSuccesfully = false;
            bool result = true;
            foreach (var sms in messagesList)
            {
                sentSuccesfully = SendAndInsertSms(domainMessage);
                if (!sentSuccesfully && result)
                {
                    result = false;
                }
            }
            //
            if (!result)
                return State.Failed;
            return State.Success;
        }

        internal List<string> SplitIntoChunks(string str, int chunkSize)
        {
            List<string> arrayOfMsgs = new List<string>();
            int countMsgs = !string.IsNullOrEmpty(str) ? (str.Length / chunkSize) + (str.Length % chunkSize > 0 ? 1 : 0) : 1;
            for (int i = 0; i < countMsgs; i++)
            {
                arrayOfMsgs.Add(string.Concat(str.ToCharArray().Skip(i * chunkSize).Take(chunkSize)));
            }
            return arrayOfMsgs;
        }


        internal DomainModel.Sms[] CreateChunks(DomainModel.Sms domainMessage)
        {
            string[] chunks = SplitIntoChunks(domainMessage.Text, 160).ToArray();
            List<DomainModel.Sms> messagesList = new List<DomainModel.Sms>();

            for (int i = 0; i < chunks.Count(); i++)
            {

                messagesList.Add(new DomainModel.Sms()
                {
                    Country = domainMessage.Country,
                    From = domainMessage.From,
                    MobileCountryCode = domainMessage.MobileCountryCode,
                    Sent = domainMessage.Sent,
                    State = domainMessage.State,
                    To = domainMessage.To,
                    Text = chunks[i]
                });
            }
            return messagesList.ToArray();
        }

        private bool SendAndInsertSms(DomainModel.Sms domainMessage)
        {
            bool result = _sendSmsService.SendSMS(domainMessage);
            domainMessage.State = result;
            _sendSmsService.InsertSentSms(domainMessage);
            return result;
        }

        internal string GetCountryCodeFromNumber(string phone)
        {
            string phoneNumbers = Regex.Match(phone, @"\d+").Value;
            //remove leading zeros
            string phoneNumbersClean = phoneNumbers.TrimStart('0');
            if (phoneNumbersClean.Length < 12)
                return string.Empty;
            return string.IsNullOrEmpty(phoneNumbersClean)?string.Empty: phoneNumbersClean.Substring(0, 2);
        }
    }
}
