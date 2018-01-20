﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MittoAgSMS.Services.Abstractions
{
    public interface ICountriesService
    {
        Task<MittoAgSMS.DomainModel.Country[]> GetCountries();
        Task<string> GetMccForNumber(string countryCode);
    }
}
