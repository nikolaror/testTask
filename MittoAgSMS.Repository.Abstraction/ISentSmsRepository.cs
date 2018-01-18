using MittoAgSMS.DataAccessLayer.Abstraction;
using MittoAgSMS.DomainModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MittoAgSMS.DataAccessLayer.Abstraction
{
    public interface ISentSmsRepository : IGenericRepository<DomainModel.Sms>
    {
        Sms[] FilterSentSms(SentSmsFilterRequest dlRequest);
    }
}
