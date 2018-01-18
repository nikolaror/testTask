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
    public class CountriesService : ICountriesService
    {
        private readonly ICountriesRepository _countriesRepository;
        public CountriesService(ICountriesRepository countriesRepository)
        {
            _countriesRepository = countriesRepository;
        }

        public Country[] GetCountries()
        {
            return _countriesRepository.GetAll().ToArray();
        }

        public string GetMccForNumber(string countryCode)
        {
            return _countriesRepository.FindBy(x=>x.CountryCode== countryCode).FirstOrDefault().MobileCountryCode;
        }
    }
}
