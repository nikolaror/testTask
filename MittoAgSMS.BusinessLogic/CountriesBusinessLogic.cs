using AutoMapper;
using MittoAgSMS.BusinessLogic.Abstractions;
using MittoAgSMS.BusinessModel;
using MittoAgSMS.Services.Abstractions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MittoAgSMS.BusinessLogic
{
    public class CountriesBusinessLogic : ICountriesBusinessLogic
    {
        private readonly ICountriesService _countriesService;
        private readonly ILoggerService _loggerService;

        public CountriesBusinessLogic(ICountriesService countriesService, ILoggerService loggerService)
        {
            _countriesService = countriesService;
            _loggerService = loggerService;
        }

        public Country[] GetCountries()
        {
            try
            {
                DomainModel.Country[] domainModel = new DomainModel.Country[0];
                domainModel = _countriesService.GetCountries();
                BusinessModel.Country[] blModel = Mapper.Map<MittoAgSMS.DomainModel.Country[], MittoAgSMS.BusinessModel.Country[]>(domainModel);
                return blModel;
            }
            catch (Exception ex)
            {
                _loggerService.LogError(ex.InnerException.Message);
                throw;
            }
        }


    }
}
