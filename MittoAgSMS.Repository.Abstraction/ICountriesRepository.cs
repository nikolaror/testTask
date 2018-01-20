using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MittoAgSMS.DomainModel;

namespace MittoAgSMS.DataAccessLayer.Abstraction
{

    public interface ICountriesRepository : IGenericRepository<MittoAgSMS.DomainModel.Country>
    {
        
        Task<Country[]> GetAllCountries();
    }
}
