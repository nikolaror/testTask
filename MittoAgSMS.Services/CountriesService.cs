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

        public async Task<Country[]> GetCountries()
        {
            return await _countriesRepository.GetAllCountries();
        }

        public async Task<string> GetMccForNumber(string countryCode)
        {
            var res = await _countriesRepository.FindBy(x => x.CountryCode == countryCode);
            return res.FirstOrDefault().MobileCountryCode;
        }
    }
}
