﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace StockNet.Areas.Api.Models
{
    public class WebResult
    {
        public string Code { get; set; }
        public string Message { get; set; }
        public Object Data { get; set; }
    }
}