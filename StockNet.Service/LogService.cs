using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ServiceStack.OrmLite;
using StockNet.Model;

namespace StockNet.Service
{
    public class LogService
    {
        public static void VipChangeRecord(int memberid,string membername,string oldlevel, string newlevel,int userid)
        {
            St_log log = new St_log();
            log.AddTime = DateTime.Now;
            log.LogType = 5;
            log.LogDetail = membername+"从"+oldlevel+"到"+newlevel;
            log.RelateId = memberid;
            log.AddUser = userid;
            DBService.AddEntity<St_log>(log);
        }

        public static void AdminLogin(string detail)
        {
            St_log log = new St_log();
            log.AddTime = DateTime.Now;
            log.LogType = 6;
            log.LogDetail = detail;
            DBService.AddEntity<St_log>(log);
        }
    }
}
