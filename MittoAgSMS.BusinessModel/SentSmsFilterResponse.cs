using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MittoAgSMS.BusinessModel
{
    public class SentSmsFilterResponse
    {
        public int TotalCount { get; set; }
        public MittoAgSMS.BusinessModel.SentSmsResponse[] Items { get; set; }

    }
}
