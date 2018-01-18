using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MittoAgSMS.DomainModel;

namespace MittoAgSMS.Services.Abstractions
{
    public interface IStatisticsService
    {
        List<Sms> GetStatistics(GetStatisticsRequestDomain domainrequest);
    }
}
