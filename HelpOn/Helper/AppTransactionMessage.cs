﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HelpOn.Helper
{
    public class AppTransactionMessage
    {
        public int Status { get; set; }
        public int TotalCartItem { get; set; }
        public string Message { get; set; }
    }

}