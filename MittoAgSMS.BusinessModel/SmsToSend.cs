﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MittoAgSMS.BusinessModel
{
    public class SmsToSend
    {
        public string From { get; set; }
        public string To { get; set; }
        public string Text { get; set; }
    }
}
