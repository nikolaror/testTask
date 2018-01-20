using System;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MittoAgSMS.BusinessLogic;
using MittoAgSMS.Services.Abstractions;
using Moq;

namespace MittoAgSMS.Tests
{
    [TestClass]
    public class StatisticsBusinessLogicTests
    {
        private StatisticsBusinessLogic _businessLogic;
        private readonly Mock<IStatisticsService> _statisticsService;
        private readonly Mock<ILoggerService> _logger;

        public StatisticsBusinessLogicTests()
        {
            _statisticsService = new Mock<IStatisticsService>();
            _logger = new Mock<ILoggerService>();

            _statisticsService.Setup(x => x.GetStatistics(It.IsAny<DomainModel.GetStatisticsRequestDomain>())).Returns(Task.FromResult(new System.Collections.Generic.List<DomainModel.Sms>() { new DomainModel.Sms() { } }));

            _businessLogic = new StatisticsBusinessLogic(_statisticsService.Object, _logger.Object);
        }

        [TestMethod]
        public void GetStatistics_returns_empty_element()
        {
            //assign

            //act
            var statistics = _businessLogic.GetStatistics(new BusinessModel.GetStatisticsRequest() { MccList=null});
            //assert
            Assert.IsNotNull(statistics);
        }
    }
}
