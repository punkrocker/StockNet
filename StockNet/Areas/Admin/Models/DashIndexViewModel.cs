using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using StockNet.Model;

namespace StockNet.Areas.Admin.Models
{
    public class DashIndexViewModel
    {
        public long StockTodayCount { get; set; }
        public long StockMonthCount { get; set; }
        public long MemberTodayCount { get; set; }
        public long MemberMonthCount { get; set; }

        public IList<St_good> StockLatest { get; set; }
        public IList<long> StockMonthlyCount { get; set; }
        public IList<long> MemberMonthlyCount { get; set; }
    }
}