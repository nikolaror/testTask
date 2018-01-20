using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MittoAgSMS.BusinessModel;

namespace MittoAgSMS.BusinessLogic.Abstractions
{
    public interface IStatisticsBusinessLogic
    {
        Task<Statistic[]> GetStatistics(GetStatisticsRequest request);
    }
}
