using System;
using System.Text;
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
        public void SplitIntoChunks_text_returns_messages()
        {
            //assign
            string text1 = new StringBuilder().Insert(0, "This is 10", 16).ToString();
            string text2 = new StringBuilder().Insert(0, "S", 161).ToString();
            string text3 = new StringBuilder().Insert(0, "S", 159).ToString();
            string text4 = new StringBuilder().Insert(0, "S", 160000).ToString();

            //act
            var chunks1 = _businessLogic.SplitIntoChunks(text1, 160);
            var chunks2 = _businessLogic.SplitIntoChunks(text2, 160);
            var chunks3 = _businessLogic.SplitIntoChunks(text3, 160);
            var chunks4 = _businessLogic.SplitIntoChunks(text4, 160);
            var chunks5 = _businessLogic.SplitIntoChunks(string.Empty, 160);
            //assert
            Assert.AreEqual(chunks1.Count, 1);
            Assert.AreEqual(chunks1[0].Length, 160);

            Assert.AreEqual(chunks2.Count, 2);
            Assert.AreEqual(chunks2[1].Length, 1);

            Assert.AreEqual(chunks3.Count, 1);
            Assert.AreEqual(chunks3[0].Length, 159);

            Assert.AreEqual(chunks4.Count, 1000);

            Assert.AreEqual(chunks5.Count, 1);
        }

        [TestMethod]
        public void GetCountryCodeForNumber_returns_countryCode()
        {
            //assign
            string phone1 = "test";
            string phone2 = "test444422223333333";
            string phone3 = "2222111111111111111";
            string phone4 = "+4988888888888888";
            string phone5 = "00787872222222227";
            string phone6 = "078787k";

            //act
            var mcc1 = _businessLogic.GetCountryCodeFromNumber(phone1);
            var mcc2 = _businessLogic.GetCountryCodeFromNumber(phone2);
            var mcc3 = _businessLogic.GetCountryCodeFromNumber(phone3);
            var mcc4 = _businessLogic.GetCountryCodeFromNumber(phone4);
            var mcc5 = _businessLogic.GetCountryCodeFromNumber(phone5);
            var mcc6 = _businessLogic.GetCountryCodeFromNumber(phone6);
            //assert
            Assert.AreEqual(mcc1, string.Empty);
            Assert.AreEqual(mcc2, "44");
            Assert.AreEqual(mcc3, "22");
            Assert.AreEqual(mcc4, "49");
            Assert.AreEqual(mcc5, "78");
            Assert.AreEqual(mcc6, string.Empty);
        }

        [TestMethod]
        public void SendSMS_longer_than_160_return_success()
        {
            //assign
            //act
             var status = _businessLogic.SendSMS(new BusinessModel.SmsToSend() { From = "The sender", To = "+4449989989", Text = new StringBuilder().Insert(0, "This is 10", 31).ToString()  });
            //assert
            Assert.AreEqual(status, BusinessModel.State.Success);
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
