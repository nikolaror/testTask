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

        [ActionName("send")]
        [HttpGet]
        public IHttpActionResult SendSMS([FromUri]SmsToSend message)
        {

            if (message == null)
                return BadRequest();
            try
            {
                return Ok(_sendSmsBusinessLogic.SendSMS(message));
            }
            catch (Exception ex)
            {
                return InternalServerError();
            }
        }

        [ActionName("sent")]
        [HttpGet]
        public IHttpActionResult GetSentSms([FromUri]SentSmsFilterRequest request)
        {
            if (request == null)
                return BadRequest();
            try
            {
                return Ok(_sendSmsBusinessLogic.GetSentSms(request));
            }
            catch (Exception ex)
            {
                return InternalServerError();
            }
        }
    }
}
