using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MittoAgSMS.BusinessModel
{
    public class SentSmsResponse
    {
        public string From { get; set; }
        public string To { get; set; }
        public string Text { get; set; }
        public string MobileCountryCode { get; set; }
        public System.DateTime Sent { get; set; }
        public bool State { get; set; }
    }
}
