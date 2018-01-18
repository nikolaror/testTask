using MittoAgSMS.DataAccessLayer.Abstraction;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MittoAgSMS.DataAccessLayer
{
    public class StatisticsRepository : GenericRepository<DomainModel.Sms>, IStatisticsRepository
    {
        public StatisticsRepository(ModelEntities entities) : base(entities)
        {
        }
    }
}
