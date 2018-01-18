using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MittoAgSMS.Services.Abstractions
{
    public interface ICountriesService
    {
        MittoAgSMS.DomainModel.Country[] GetCountries();
        string GetMccForNumber(string countryCode);
    }
}
