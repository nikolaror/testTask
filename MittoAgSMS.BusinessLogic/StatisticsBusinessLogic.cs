using MittoAgSMS.BusinessLogic.Abstractions;
using MittoAgSMS.BusinessModel;
using MittoAgSMS.DomainModel;
using MittoAgSMS.Services.Abstractions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MittoAgSMS.BusinessLogic
{
    public class StatisticsBusinessLogic : IStatisticsBusinessLogic
    {
        private readonly IStatisticsService _statisticsService;
        public StatisticsBusinessLogic(IStatisticsService statisticsService)
        {
            _statisticsService = statisticsService;
        }

        public Statistic[] GetStatistics(GetStatisticsRequest request)
        {
            DomainModel.GetStatisticsRequestDomain domainrequest = new DomainModel.GetStatisticsRequestDomain()
            {
                DateFrom = request.DateFrom,
                DateTo = request.DateTo,
                MccList = request.MccList?[0]?.Split(',')?.ToList<string>()??new List<string>()
            };
            List<DomainModel.Sms> sentSmsStatistics = _statisticsService.GetStatistics(domainrequest);

            var query = (from t in sentSmsStatistics
                         group t by new { t.Sent.Value.Date, t.MobileCountryCode, t.Country.PricePerSms }
             into grp
                         select new Statistic
                         {
                             Day=grp.Key.Date,
                             Mcc=grp.Key.MobileCountryCode.Trim(),
                             PricePerSms = grp.Key.PricePerSms.Value,
                             TotalPrice = grp.Sum(t => t.Country.PricePerSms).Value,
                             Count = grp.Count()
                         }).ToArray();
            return query;
        }
    }
}
