using MittoAgSMS.DataAccessLayer.Abstraction;
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

        //public UserCertificates GetByCertificateThumbprint(string certificateThumbprint)
        //{
        //    return GetAll().AsQueryable().Where(x => x.CertificateThumbprint.ToLower() == certificateThumbprint.ToLower()).FirstOrDefault();
        //}
    }
}
