using MittoAgSMS.BusinessLogic.Abstractions;
using MittoAgSMS.BusinessModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace MittoAgSMS.Controllers
{
    public class CommonController : ApiController
    {
        private readonly ICountriesBusinessLogic _countriesBusinessLogic;
        private readonly IStatisticsBusinessLogic _statisticsBusinessLogic;

        public CommonController(ICountriesBusinessLogic countriesBusinessLogic, IStatisticsBusinessLogic statisticsBusinessLogic)
        {
            _countriesBusinessLogic = countriesBusinessLogic;
            _statisticsBusinessLogic = statisticsBusinessLogic;
        }

        [ActionName("statistics")]
        [HttpGet]
        public IHttpActionResult GetStatistics([FromUri]GetStatisticsRequest request)
        {
            if (request == null)
                return BadRequest();
            try
            {
                return Ok(_statisticsBusinessLogic.GetStatistics(request));
            }
            catch (Exception ex)
            {
                return InternalServerError();
            }
        }

        [ActionName("countries")]
        [HttpGet]
        public IHttpActionResult GetCountries()
        {
            try
            {
                return Ok(_countriesBusinessLogic.GetCountries());
            }
            catch (Exception ex)
            {
                return InternalServerError();
            }
        }
    }
}
