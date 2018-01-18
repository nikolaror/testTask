using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MittoAgSMS.BusinessModel
{
    public class Country
    {
        public string Mcc { get; set; }
        public string Cc { get; set; }
        public string Name { get; set; }
        public decimal PricePerSms { get; set; }
    }
}
