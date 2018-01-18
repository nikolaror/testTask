using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MittoAgSMS.DataAccessLayer.Abstraction
{

    public interface ICountriesRepository : IGenericRepository<MittoAgSMS.DomainModel.Country>
    {
       // UserCertificates GetByCertificateThumbprint(string certificateThumbprint);
    }
}
