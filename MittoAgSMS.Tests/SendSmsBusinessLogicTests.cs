using System;
using AutoMapper;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MittoAgSMS.BusinessLogic;
using MittoAgSMS.Services.Abstractions;
using Moq;

namespace MittoAgSMS.Tests
{
    [TestClass]
    public class SendSmsBusinessLogicTests
    {
        private SendSmsBusinessLogic _businessLogic;
        private readonly Mock<ISendSmsService> _sendSmsService;
        private readonly Mock<IGetSentSmsService> _getSentService;
        private readonly Mock<ICountriesService> _countiresService;

        public SendSmsBusinessLogicTests()
        {
            _sendSmsService = new Mock<ISendSmsService>();
            _getSentService = new Mock<IGetSentSmsService>();
            _countiresService = new Mock<ICountriesService>();
            _sendSmsService.Setup(x => x.InsertSentSms(It.IsAny<DomainModel.Sms>()));
            _sendSmsService.Setup(x => x.SendSMS(It.IsAny<DomainModel.Sms>())).Returns(true);
            _countiresService.Setup(x => x.GetMccForNumber(It.IsAny<string>())).Returns("32");

            _businessLogic = new SendSmsBusinessLogic(_sendSmsService.Object, _getSentService.Object, _countiresService.Object);
        }

        [TestMethod]
        [ExpectedException(typeof(Exception), "Mcc not valid!")]
        public void SendSMS_country_code_not_valid()
        {
            //assign
            _countiresService.Setup(x => x.GetMccForNumber(It.IsAny<string>())).Returns("");
            _businessLogic = new SendSmsBusinessLogic(_sendSmsService.Object, _getSentService.Object, _countiresService.Object);
            //act
            var countries = _businessLogic.SendSMS(new BusinessModel.SmsToSend() { From = "The sender", To = "+4449989989", Text = "this is test" });
            //assert

        }

        [TestMethod]
        [ExpectedException(typeof(Exception),"Receiver not valid!")]
        public void SendSMS_wrong_number_returns_exception()
        {
            //assign

            //act
            var countries = _businessLogic.SendSMS(new BusinessModel.SmsToSend() { From = "The sender", To = "p4449989989", Text = "this is test" });
            //assert

        }

        [TestMethod]
        public void SendSMS_text_empty_returns_true()
        {
            //assign

            //act
            var smsState = _businessLogic.SendSMS(new BusinessModel.SmsToSend() { From = "The sender", To = "+4449989989", Text = string.Empty });
            //assert
            Assert.AreEqual(smsState, BusinessModel.State.Success);
        }

        [TestMethod]
        [ExpectedException(typeof(Exception), "Receiver not valid!")]
        public void SendSMS_phone_empty_returns_excpetion()
        {
            //assign

            //act
            var smsState = _businessLogic.SendSMS(new BusinessModel.SmsToSend() { From = "The sender", To = string.Empty, Text = string.Empty });
            //assert
        }

        [TestMethod]
        [ExpectedException(typeof(Exception), "Receiver not valid!")]
        public void SendSMS_emptysms_returns_exception()
        {
            //assign

            //act
            var smsState = _businessLogic.SendSMS(new BusinessModel.SmsToSend() {});
            //assert
        }

        [TestMethod]
        [ExpectedException(typeof(Exception), "Receiver not valid!")]
        public void SendSMS_null_returns_exception()
        {
            //assign

            //act
            var smsState = _businessLogic.SendSMS(null);
            //assert
        }
    }
}
