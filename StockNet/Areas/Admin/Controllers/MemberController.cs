using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using StockNet.Service;
using StockNet.Areas.Admin.Models;
using StockNet.Models;
using StockNet.Model;

namespace StockNet.Areas.Admin.Controllers
{
    public class MemberController : ControllerBase
    {
        // GET: Admin/Owner
        public ActionResult Index(int? vip)
        {
            ViewBag.VIP = vip.HasValue && vip.Value == 1 ? 1 : 0;
            return View();
        }

        [HttpPost]
        public JsonResult List(PagingRequest pg,int? vip)
        {
            var res = new PagingReply();
            res.iTotalRecords = MemberService.GetMemberCount(pg.sSearch);
            if (res.iTotalRecords > 0)
            {
                IList<St_member> members = null;
                if(vip.HasValue && vip.Value == 1)
                    members = MemberService.GetVipMembers(pg.iDisplayStart, pg.iDisplayLength, pg.sSearch);
                else
                    members = MemberService.GetMembers(pg.iDisplayStart, pg.iDisplayLength, pg.sSearch);
                var users = MemberService.GetUsers(members.Select(s => s.UserId.HasValue ? s.UserId.Value : 0).ToList());
                //if (vip.HasValue && vip.Value == 1)
                //{
                //    var vipmembers=members.Where(ct=>ct.Level != "普通会员" && ct.VipOverDate.HasValue && ct.VipOverDate.Value > DateTime.Now).ToList();
                //    res.iTotalRecords = vipmembers.Count;
                //    res.aaData = vipmembers.Select(ct => new object[] { ct.RealName, GetUserName(ct.UserId, users), ct.Level, ct.HadVerify.HasValue && ct.HadVerify.Value ? "已验证" : "未验证", GetContactInfo(ct.Mobile, ct.Tel, ct.QQ, ct.Email, ct.Wechat), ct.AddTime.ToString(), ct.Id }).ToList();
                //}
                //else
                res.aaData = members.Select(ct => new object[] { ct.RealName, GetUserName(ct.UserId, users), ct.Level != "普通会员" && ct.VipOverDate.HasValue && ct.VipOverDate.Value > DateTime.Now ? ct.Level : "普通会员", ct.HadVerify.HasValue && ct.HadVerify.Value ? "已验证" : "未验证", GetContactInfo(ct.Mobile, ct.Tel, ct.QQ, ct.Email, ct.Wechat), ct.AddTime.ToString(), ct.Id }).ToList();
            }
            res.iTotalDisplayRecords = res.iTotalRecords;
            res.sEcho = pg.sEcho.ToString();

            return Json(res);
        }

        private string GetUserName(int? userID,IList<St_user> users)
        {
            var username = "";
            if(userID.HasValue &&userID.Value>0&&users!=null&&users.Count>0)
            {
                username = users.Where(s => s.Id == userID.Value).Select(s => s.LoginName).SingleOrDefault();
            }
            return username;
        }

        private string GetContactInfo(string mobile,string tel,string qq,string email,string wechat)
        {
            string res = "";
            if (!string.IsNullOrEmpty(mobile))
            {
                res += " 手机:"+mobile;
            }
            if (!string.IsNullOrEmpty(tel))
            {
                res += " 电话:" + tel;
            }
            if (!string.IsNullOrEmpty(qq))
            {
                res += " QQ:" + qq;
            }
            if (!string.IsNullOrEmpty(email))
            {
                res += " 邮箱:" + email;
            }
            if (!string.IsNullOrEmpty(wechat))
            {
                res += " 微信:" + wechat;
            }
            return res;
        }

        public ActionResult Edit(int id)
        {
            var vm = new MemberEditViewModel();
            vm.Member = DBService.GetEntity<St_member>(id);
            if(vm.Member.UserId.HasValue)
                vm.User = DBService.GetEntity<St_user>(vm.Member.UserId.Value);
            return View(vm);
        }

        [HttpPost]
        [ActionName("Edit")]
        [ValidateAntiForgeryToken]
        public ActionResult EditPOST(int id,MemberEditViewModel model)
        {
            if(ModelState.IsValid)
            {
                var member = DBService.GetEntity<St_member>(id);
                member.RealName = model.username;//change user name too
                var oldlv = member.Level;
                var now = DateTime.Now;
                if(member.Level != model.level)
                {                    
                    if(model.level=="普通会员")
                    {
                        member.VipOverDate = null;
                    }
                    else
                    {
                        member.VipOverDate = now.AddYears(1);
                    }
                    member.Level = model.level;
                    LogService.VipChangeRecord(member.Id,member.RealName, oldlv, model.level,GetCurUserId());                    
                }

                member.HadVerify = model.verifyed == 1;
                DBService.UpdateEntity<St_member>(member);

                if (member.UserId.HasValue)
                {
                    if (model.userlock == 1)
                        UserService.LockUser(member.UserId.Value);
                    else
                        UserService.UnLockUser(member.UserId.Value);
                }

                return RedirectToAction("Index");
            }
            return View(model);
        }

        public ActionResult Del(int id)
        {
            if(id>0)
            {
                var member = DBService.GetEntity<St_member>(id);
                if (member.Level == "普通会员" && !member.VipOverDate.HasValue)
                    MemberService.DelMember(id,member.UserId);
            }
            return RedirectToAction("Index");
        }
    }
}