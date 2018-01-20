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
        private readonly ILoggerService _loggerService;

        public StatisticsBusinessLogic(IStatisticsService statisticsService, ILoggerService loggerService)
        {
            _statisticsService = statisticsService;
            _loggerService = loggerService;
        }

        public Statistic[] GetStatistics(GetStatisticsRequest request)
        {
            try
            {
                DomainModel.GetStatisticsRequestDomain domainrequest = new DomainModel.GetStatisticsRequestDomain()
                {
                    DateFrom = request.DateFrom,
                    DateTo = request.DateTo,
                    MccList = request.MccList?[0]?.Split(',')?.ToList<string>() ?? new List<string>()
                };
                List<DomainModel.Sms> sentSmsStatistics = _statisticsService.GetStatistics(domainrequest);
                if (sentSmsStatistics == null)
                    return new Statistic[0];

                //var query = (from t in sentSmsStatistics
                //             group t by new { t.Sent.Date, t.MobileCountryCode, t.Country.PricePerSms }
                // into grp
                //             select new Statistic
                //             {
                //                 Day = grp.Key.Date,
                //                 Mcc = grp.Key.MobileCountryCode?.Trim(),
                //                 PricePerSms = grp.Key.PricePerSms.Value,
                //                 TotalPrice = grp.Sum(t => t.Country?.PricePerSms).Value,
                //                 Count = grp.Count()
                //             }).ToArray();


                var query = sentSmsStatistics.GroupBy(
                    p => new { Sent = p.Sent == null ? p.Sent.Date : DateTime.Now, Mcc = p.MobileCountryCode ?? string.Empty, Price = p.Country != null ? p.Country.PricePerSms : 0 },
                        (key, g) => new Statistic
                        {
                            Day = key.Sent,
                            Mcc = key.Mcc?.Trim(),
                            PricePerSms = key.Price.Value,
                            TotalPrice = g.Sum(t => t.Country != null ? t.Country.PricePerSms : 0).Value,
                            Count = g.Count()
                        }).ToArray();
                return query;
            }
            catch(Exception ex)
            {
                _loggerService.LogError(ex.InnerException.Message);
                throw;
            }
        }
    }
}
