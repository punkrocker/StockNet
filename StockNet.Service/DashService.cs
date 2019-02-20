using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ServiceStack.OrmLite;
using StockNet.Model;

namespace StockNet.Service
{
    public class DashService
    {
        public static IList<St_good> GetLatestStocks()
        {
            using (var db = DBService.OpenDb())
            {
                return db.Select<St_good>(cc => cc.Limit(5).OrderByDescending(fs => fs.Id));
            }
        }

        public static CountDTO GetStockMemberCount()
        {
            CountDTO cdto = new CountDTO();
            DateTime now = DateTime.Now;
            using (var db = DBService.OpenDb())
            {
                cdto.StockTodayCount = db.Count<St_good>(cc => cc.AddTime >= new DateTime(now.Year, now.Month, now.Day));
                cdto.StockMonthCount = db.Count<St_good>(cc => cc.AddTime > new DateTime(now.Year, now.Month, 1));
                cdto.MemberTodayCount = db.Count<St_member>(cc => cc.AddTime > new DateTime(now.Year, now.Month, now.Day));
                cdto.MemberMonthCount = db.Count<St_member>(cc => cc.AddTime > new DateTime(now.Year, now.Month, 1));
            }
            return cdto;
        }

        public static long[] GetMemberMonthlyCount()
        {
            var res = new long[12];
            var mcs = new List<MonthlyCount>();
            var yeardate = new DateTime(DateTime.Now.Year, 1, 1, 0, 0, 0);
            using (var db = DBService.OpenDb())
            {
                mcs = db.SqlList<MonthlyCount>("SELECT month(AddTime) as Month,count(Id) as Count FROM st_member where AddTime>='" + yeardate + "' group by month(AddTime);");
            }
            if (mcs != null && mcs.Count > 0)
            {
                for (var i = 0; i < 12; i++)
                {
                    foreach(var mc in mcs)
                    {
                        if (mc.Month == i + 1)
                        {
                            res[i] = mc.Count;
                        }
                    }
                }
            }
            return res;
        }

        public static long[] GetStockMonthlyCount()
        {
            var res = new long[12];
            var mcs = new List<MonthlyCount>();
            var yeardate = new DateTime(DateTime.Now.Year, 1, 1, 0, 0, 0);
            using (var db = DBService.OpenDb())
            {
                mcs = db.SqlList<MonthlyCount>("SELECT month(AddTime) as Month,count(Id) as Count FROM st_goods where AddTime>='" + yeardate + "' group by month(AddTime);");
            }
            if (mcs != null && mcs.Count > 0)
            {
                for (var i = 0; i < 12; i++)
                {
                    foreach (var mc in mcs)
                    {
                        if (mc.Month == i + 1)
                        {
                            res[i] = mc.Count;
                        }
                    }
                }
            }
            return res;
        }
    }
}
