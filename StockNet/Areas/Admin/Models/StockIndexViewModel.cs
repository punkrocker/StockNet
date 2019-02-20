using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using StockNet.Model;

namespace StockNet.Areas.Admin.Models
{
    public class StockIndexViewModel
    {
        public St_member Member { get; set; }

        public int? MemberId { get; set; }
        public int? CatId { get; set; }
        public string Search { get; set; }
    }
}