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

        /// <summary>
        /// Method for getting statistics
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [ActionName("statistics")]
        [HttpGet]
        public async System.Threading.Tasks.Task<IHttpActionResult> GetStatistics([FromUri]GetStatisticsRequest request)
        {
            if (request == null)
                return BadRequest();
            try
            {
                return Ok(await _statisticsBusinessLogic.GetStatistics(request));
            }
            catch (Exception ex)
            {
                return InternalServerError();
            }
        }
        /// <summary>
        /// Method for returning data about countries
        /// </summary>
        /// <returns></returns>
        [ActionName("countries")]
        [HttpGet]
        public async System.Threading.Tasks.Task<IHttpActionResult> GetCountries()
        {
            try
            {
                return Ok(await _countriesBusinessLogic.GetCountries());
            }
            catch (Exception ex)
            {
                return InternalServerError();
            }
        }
    }
}
