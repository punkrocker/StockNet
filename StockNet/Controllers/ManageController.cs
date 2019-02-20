using System;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using Microsoft.Owin.Security;
using StockNet.Models;
using StockNet.Model;
using StockNet.Service;

namespace StockNet.Controllers
{
    [Authorize(Roles="0")]
    [TokenFilter]
    public class ManageController : ControllerBase
    {
        public ActionResult Index()
        {
            var vm = new MemberIndexViewModel();
            var uid = GetCurUserId();
            if (uid > 0)
            {
                var member = MemberService.GetMemberFromUser(uid);
                if (member != null && member.Id > 0)
                {
                    vm.Level = member.Level != "普通会员" && member.VipOverDate.HasValue && member.VipOverDate.Value > DateTime.Now ? member.Level : "普通会员";

                }
            }
            return View(vm);
        }

        public ActionResult Stocks(int? p)
        {
            var vm = new MemberStockViewModel();
            vm.PageSize = 40;
            vm.CurPageIndex = p.HasValue && p.Value > 1 ? p.Value : 1;
            var memberid = 0;
            var uid = GetCurUserId();
            if (uid > 0)
            {
                var member = MemberService.GetMemberFromUser(uid);
                if (member != null && member.Id > 0)
                {
                    memberid = member.Id;
                }
            }
            vm.RecordCount = StockService.GetMemberTotalStockCount(memberid, "");
            vm.PageTotal = (int)Math.Ceiling((double)vm.RecordCount / 40);
            if (vm.CurPageIndex > vm.PageTotal)
                vm.CurPageIndex = vm.PageTotal;
            if (vm.RecordCount > 0)
                vm.CurPageStocks = StockService.GetMemberPagedStocks(memberid, (vm.CurPageIndex - 1) * vm.PageSize, vm.PageSize, "");
            return View(vm);
        }

        public ActionResult VIP()
        {
            return RedirectToAction("Index");
        }

        public ActionResult ChangeInfo()
        {
            var vm = new ChangeInfoViewModel();
            var uid = GetCurUserId();
            if (uid > 0)
            {
                var member = MemberService.GetMemberFromUser(uid);
                if (member != null && member.Id > 0)
                {
                    vm.Addr = member.Addr;
                    vm.Email = member.Email;
                    vm.Mobile = member.Mobile;
                    vm.QQ = member.QQ;
                    vm.RealName = member.RealName;
                    vm.Other = member.Remark;
                    vm.Tel = member.Tel;
                    vm.Wechat = member.Wechat;
                }
            }
            return View(vm);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult ChangeInfo(ChangeInfoViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }
            var uid = GetCurUserId();
            if (uid > 0 && !string.IsNullOrEmpty(model.Email) && !string.IsNullOrEmpty(model.RealName))
            {
                var member = MemberService.GetMemberFromUser(uid);
                if (member != null && member.Id > 0)
                {
                    member.Addr = model.Addr;
                    member.Email = model.Email;
                    member.Mobile = model.Mobile;
                    member.QQ = model.QQ;
                    member.RealName = model.RealName;
                    member.Remark = model.Other;
                    member.Tel = model.Tel;
                    member.Wechat = model.Wechat;
                    DBService.UpdateEntity<St_member>(member);
                }
                return RedirectToAction("Index");
            }
            ModelState.AddModelError("", "无法修改联系方式！");
            return View(model);
        }

        public ActionResult ChangePassword()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult ChangePassword(ChangePasswordViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }
            var uid = GetCurUserId();
            if (uid>0&&!string.IsNullOrEmpty(model.OldPassword)&&!string.IsNullOrEmpty(model.NewPassword))
            {
                UserService.ChangeUserPass(uid, model.OldPassword, model.NewPassword);
                return RedirectToAction("Index");
            }
            ModelState.AddModelError("", "无法修改密码！");
            return View(model);
        }
    }
}