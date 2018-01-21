using MittoAgSMS.BusinessLogic.Abstractions;
using MittoAgSMS.BusinessModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Filters;


namespace MittoAgSMS.Controllers
{
    public class SMSController : ApiController
    {
        private readonly ISendSmsBusinessLogic _sendSmsBusinessLogic;

        public SMSController(ISendSmsBusinessLogic sendSmsBusinessLogic)
        {
            _sendSmsBusinessLogic = sendSmsBusinessLogic;
        }
        /// <summary>
        /// method for sending sms
        /// </summary>
        /// <param name="message"></param>
        /// <returns></returns>
        [ActionName("send")]
        [HttpGet]
        public async System.Threading.Tasks.Task<IHttpActionResult> SendSMS([FromUri]SmsToSend message)
        {

            if (message == null)
                return BadRequest();
            try
            {
                return Ok(await _sendSmsBusinessLogic.SendSMS(message));
            }
            catch (Exception ex)
            {
                return InternalServerError();
            }
        }
        /// <summary>
        /// method for retreiving sent sms
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [ActionName("sent")]
        [HttpGet]
        public async System.Threading.Tasks.Task<IHttpActionResult> GetSentSms([FromUri]SentSmsFilterRequest request)
        {
            if (request == null)
                return BadRequest();
            try
            {
                return Ok(await _sendSmsBusinessLogic.GetSentSms(request));
            }
            catch (Exception ex)
            {
                return InternalServerError();
            }
        }
    }
}
