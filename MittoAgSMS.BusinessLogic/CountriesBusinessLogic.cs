﻿using AutoMapper;
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

        public CountriesBusinessLogic(ICountriesService countriesService)
        {
            _countriesService = countriesService;
        }

        public Country[] GetCountries()
        {
            DomainModel.Country[] domainModel = new DomainModel.Country[0];
            domainModel = _countriesService.GetCountries();
            BusinessModel.Country[] blModel = Mapper.Map<MittoAgSMS.DomainModel.Country[], MittoAgSMS.BusinessModel.Country[]>(domainModel);
            return blModel;
        }


    }
}
