using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MittoAgSMS.DomainModel
{
        public class Country
        {
            public string Name { get; set; }
            public string MobileCountryCode { get; set; }
            public string CountryCode { get; set; }
            public Nullable<decimal> PricePerSms { get; set; }
        }
}
