using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StockNet.Service
{
    public class CountDTO
    {
        public long StockTodayCount { get; set; }
        public long StockMonthCount { get; set; }
        public long MemberTodayCount { get; set; }
        public long MemberMonthCount { get; set; }
    }

    public class MonthlyCount
    {
        public int Month { get; set; }
        public long Count { get; set; }
    }
}
