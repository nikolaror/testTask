using MittoAgSMS.DataAccessLayer.Abstraction;
using MittoAgSMS.DomainModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MittoAgSMS.DataAccessLayer
{
    public class CountriesRepository : GenericRepository<MittoAgSMS.DomainModel.Country>, ICountriesRepository
    {
        public CountriesRepository(ModelEntities entities) : base(entities)
        {
        }

        public async Task<Country[]> GetAllCountries()
        {
            var res = await GetAll();
            return res.AsQueryable().ToArray();
        }
    }
}
