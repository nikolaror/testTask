using MittoAgSMS.DataAccessLayer.Abstraction;
using MittoAgSMS.DomainModel;
using MittoAgSMS.Services.Abstractions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace MittoAgSMS.Services
{
    public class StatisticsService : IStatisticsService
    {
        private readonly IStatisticsRepository _statisticsRepository;

        public StatisticsService(IStatisticsRepository statisticsRepository)
        {
            _statisticsRepository = statisticsRepository;

        }
        public List<Sms> GetStatistics(GetStatisticsRequestDomain domainrequest)
        {
            var sentSmsStatistic = _statisticsRepository.GetAll("Country").AsQueryable().Where(x => domainrequest.DateFrom <= x.Sent && x.Sent <= domainrequest.DateTo && domainrequest.MccList.Count>0? domainrequest.MccList.Contains(x.MobileCountryCode) : 1==1).ToList();
            return sentSmsStatistic;
        }
    }
}
