using System;
using AutoMapper;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MittoAgSMS.BusinessLogic;
using MittoAgSMS.DomainModel;
using MittoAgSMS.Services.Abstractions;
using Moq;

namespace MittoAgSMS.Tests
{
    [TestClass]
    public class CountriesBusinessLogicTests
    {
        private CountriesBusinessLogic _businessLogic;
        private readonly Mock<ICountriesService> _countriesService;
        private readonly Mock<ILoggerService> _logger;

        private readonly Mock<Mapper> _automapper;


        public CountriesBusinessLogicTests()
        {
            _countriesService = new Mock<ICountriesService>();
            _logger = new Mock<ILoggerService>();
            Mapper.Initialize(config =>
            {
                config.CreateMap<MittoAgSMS.DomainModel.Country, MittoAgSMS.BusinessModel.Country>().
                ForMember(source => source.Cc, destination => destination.MapFrom(x => x.CountryCode)).
                ForMember(source => source.Mcc, destination => destination.MapFrom(x => x.MobileCountryCode)).
                ForMember(source => source.Name, destination => destination.MapFrom(x => x.Name)).
                ForMember(source => source.PricePerSms, destination => destination.MapFrom(x => x.PricePerSms));
            });
        }

        [TestMethod]
        public void GetCountries_one_element_returns_length()
        {
            //assign
            _countriesService.Setup(x => x.GetCountries()).Returns(new Country[] { new Country() { Name = "Austria", MobileCountryCode = "9999" } });
            _businessLogic = new CountriesBusinessLogic(_countriesService.Object, _logger.Object);
            //act
            var countries = _businessLogic.GetCountries();
            //assert
            Assert.AreEqual(countries.Length, 1);
        }
    }
}
