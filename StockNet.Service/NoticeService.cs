using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ServiceStack.OrmLite;
using StockNet.Model;

namespace StockNet.Service
{
    public class NoticeService
    {
        public static int GetNoticeCount(string search)
        {
            using (var db = DBService.OpenDb())
            {
                if (!string.IsNullOrEmpty(search))
                    return (int)db.Count<St_notice>(fs => fs.Title.Contains(search));
                else
                    return (int)db.Count<St_notice>();
            }
        }
        public static IList<St_notice> GetNotices(int start, int limit, string search)
        {
            using (var db = DBService.OpenDb())
            {
                if (!string.IsNullOrEmpty(search))
                    return db.Select<St_notice>(fs => fs.Limit(start, limit).Where(ct => ct.Title.Contains(search)).OrderByDescending(ct => ct.Id));
                else
                    return db.Select<St_notice>(fs => fs.Limit(start, limit).OrderByDescending(ct => ct.Id));
            }
        }

        public static IList<St_notice> GetLatestNotices(int limit,int noticeType)
        {
            using (var db = DBService.OpenDb())
            {
                return db.Select<St_notice>(fs => fs.Where(ct=>ct.NoticeType==noticeType).Limit(limit).OrderByDescending(ct => ct.Id));                
            }
        }
    }
}
