using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MittoAgSMS.DomainModel
{
    public class Sms
    {
        public int Id { get; set; }
        public string From { get; set; }
        public string To { get; set; }
        public string Text { get; set; }
        public string MobileCountryCode { get; set; }
        public Nullable<System.DateTime> Sent { get; set; }
        public virtual Country Country { get; set; }
        public bool State { get; set; }

        public override string ToString()
        {
            return string.Format("Message|| from:{0}|| countryCode:{1}|| to:{2}|| text:{3}|| state:{4}", From, MobileCountryCode, To, Text, State);
        }
    }
}
