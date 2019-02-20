using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ServiceStack.OrmLite;
using StockNet.Model;

namespace StockNet.Service
{
    public class MemberService
    {
        public static int GetMemberCount(string search)
        {
            using (var db = DBService.OpenDb())
            {
                if (!string.IsNullOrEmpty(search))
                    return (int)db.Count<St_member>(fs => fs.RealName.Contains(search) || fs.Mobile.Contains(search));
                else
                    return (int)db.Count<St_member>();
            }
        }
        public static IList<St_member> GetMembers(int start, int limit, string search)
        {
            using (var db = DBService.OpenDb())
            {
                if (!string.IsNullOrEmpty(search))
                    return db.Select<St_member>(fs => fs.Limit(start, limit).Where(ct => ct.RealName.Contains(search) || ct.Mobile.Contains(search)).OrderByDescending(ct => ct.Id));
                else
                    return db.Select<St_member>(fs => fs.Limit(start, limit).OrderByDescending(ct => ct.Id));
            }
        }

        public static IList<St_member> GetVipMembers(int start, int limit, string search)
        {
            using (var db = DBService.OpenDb())
            {
                var now = DateTime.Now;
                if (!string.IsNullOrEmpty(search))
                    return db.Select<St_member>(fs => fs.Limit(start, limit).Where(ct => (ct.RealName.Contains(search) || ct.Mobile.Contains(search)) && ct.Level != "普通会员" && ct.VipOverDate != null && ct.VipOverDate > now).OrderByDescending(ct => ct.Id));
                else
                    return db.Select<St_member>(fs => fs.Limit(start, limit).Where(ct=>ct.Level != "普通会员" && ct.VipOverDate != null && ct.VipOverDate > now).OrderByDescending(ct => ct.Id));
            }
        }

        public static void DelMember(int memberID,int? userID)
        {
            if(memberID>0)
            {
                using(var db=DBService.OpenDb())
                {
                    db.Delete<St_good>(s => s.MemberId == memberID);//
                    if(userID.HasValue&&userID.Value>0)
                        db.Delete<St_user>(s=>s.Id==userID.Value);
                    db.Delete<St_member>(ent => ent.Id == memberID);
                }
            }
        }

        public static St_member GetMemberFromUser(int userid)
        {
            if (userid > 0)
            {
                using (var db = DBService.OpenDb())
                {
                    return db.SingleOrDefault<St_member>("UserId={0}", userid);
                }
            }
            else
                return null;
        }

        public static IList<St_user> GetUsers(IList<int> userids)
        {
            if (userids != null && userids.Count > 0)
            {
                using (var db = DBService.OpenDb())
                {
                    return db.Select<St_user>().Where(s => userids.Contains(s.Id)).ToList();
                }
            }
            else
                return null;
        }

        public static IList<St_member> GetMembersByIds(IList<int> memberIds)
        {
            if (memberIds != null && memberIds.Count > 0)
            {
                using (var db = DBService.OpenDb())
                {
                    return db.Select<St_member>().Where(s => memberIds.Contains(s.Id)).ToList();
                }
            }
            else
                return null;
        }
    }
}
