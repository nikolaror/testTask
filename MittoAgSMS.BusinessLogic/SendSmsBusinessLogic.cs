using AutoMapper;
using MittoAgSMS.BusinessLogic.Abstractions;
using MittoAgSMS.BusinessLogic.Validators;
using MittoAgSMS.BusinessModel;
using MittoAgSMS.DomainModel;
using MittoAgSMS.Services.Abstractions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

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
            MittoAgSMS.BusinessModel.Sms[] itemsBl = Mapper.Map<MittoAgSMS.DomainModel.Sms[], MittoAgSMS.BusinessModel.Sms[]>(domainResult);
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
            string MccForPhone = _countryService.GetMccForNumber(countryCode);
            if(string.IsNullOrEmpty(MccForPhone))
            {
                throw new Exception("Mcc not valid!");
            }
            DomainModel.Sms domainMessage = new DomainModel.Sms() { From = message.From, To = message.To, Text = message.Text??string.Empty, MobileCountryCode = MccForPhone.Trim(), Sent = DateTime.Now };

            bool result = _sendSmsService.SendSMS(domainMessage);
            domainMessage.State = result;
            _sendSmsService.InsertSentSms(domainMessage);

            if (!result)
                return State.Failed;
            return State.Success;
        }

        private string GetCountryCodeFromNumber(string phone)
        {
            string phoneNumbers = Regex.Match(phone, @"\d+").Value;
            //remove leading zeros
            string phoneNumbersClean = phoneNumbers.TrimStart('0');
            return phoneNumbersClean.Substring(0, 2);
        }
    }
}
