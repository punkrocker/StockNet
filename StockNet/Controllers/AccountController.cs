using System;
using System.Globalization;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using Microsoft.Owin.Security;
using StockNet.Models;
using StockNet.Service;
using StockNet.Model;
using System.Collections.Generic;

namespace StockNet.Controllers
{
    [Authorize]
    public class AccountController : Controller
    {
        public AccountController()
        {
        }

        private IAuthenticationManager AuthenticationManager
        {
            get { return HttpContext.GetOwinContext().Authentication; }
        }

        //
        // GET: /Account/Login
        [AllowAnonymous]
        public ActionResult Login(string returnUrl)
        {
            ViewBag.ReturnUrl = returnUrl;
            if(!string.IsNullOrEmpty(returnUrl)&&(returnUrl+"/").IndexOf("/admin/",StringComparison.OrdinalIgnoreCase)>-1)
            {
                return Redirect("/Admin/Login?returnUrl="+returnUrl);
            }
            return View();
        }

        //
        // POST: /Account/Login
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public ActionResult Login(LoginViewModel model, string returnUrl)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            var identity = UserService.CheckIdentity(model.UserName, model.Password);
            if (identity != null)
            {
                //AuthenticationManager.SignOut();
                AuthenticationManager.SignIn(new AuthenticationProperties() { IsPersistent = model.RememberMe=="on" }, identity);
                return RedirectToLocal(returnUrl);
            }
            else
            {
                ModelState.AddModelError("", "无效的登录尝试。");
                return View(model);
            }
        }

        //
        // POST: /Account/LogOff
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult LogOff()
        {
            string struid = "", curtoken = "";
            int uid = 0;
            if (User.Identity.IsAuthenticated)
            {
                ClaimsIdentity ci = User.Identity as ClaimsIdentity;
                if (ci != null)
                {
                    var claim = ci.Claims.SingleOrDefault(cl => cl.Type == ClaimTypes.NameIdentifier);
                    if (claim != null)
                    {
                        struid = claim.Value;
                        int.TryParse(struid, out uid);
                    }

                    var tokenclain = ci.Claims.SingleOrDefault(cl => cl.Type == "Token");
                    if (tokenclain != null)
                        curtoken = tokenclain.Value;
                }
            }
            UserService.UpdateLoginToken(uid, curtoken, "");

            AuthenticationManager.SignOut();
            return RedirectToAction("Index", "Home");
        }

        public ActionResult Out()
        {
            return View();
        }

        private ActionResult RedirectToLocal(string returnUrl)
        {
            if (Url.IsLocalUrl(returnUrl))
            {
                return Redirect(returnUrl);
            }
            return RedirectToAction("Index", "Home");
        }

        [AllowAnonymous]
        public ActionResult Register(string returnUrl)
        {
            ViewBag.ReturnUrl = returnUrl;
            return View();
        }

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public ActionResult Register(RegisterViewModel model, string returnUrl)
        {
            if(UserService.Exists(model.UserName))
                ModelState.AddModelError("UserName", "用户名已存在！");
            if (ModelState.IsValid)
            {
                var user = new St_user();
                user.AddTime = DateTime.Now;
                user.LoginName = model.UserName;
                user.LoginPass = DBService.SHA1Hash(model.Password);
                user.Name = model.RealName;
                user.Email = model.Email;
                user.RoleId = 0;
                user.Status = 0;
                int uid = DBService.AddEntity<St_user>(user,true);

                if (uid > 0)
                {
                    var member = new St_member();
                    member.Addr = model.Addr;
                    member.AddTime = DateTime.Now;
                    member.Email = model.Email;
                    member.HadVerify = false;
                    member.Level = "普通会员";
                    member.Mobile = model.Mobile;
                    member.QQ = model.QQ;
                    member.RealName = model.RealName??model.UserName;
                    member.Remark = model.Other;
                    member.Tel = model.Tel;
                    member.UserId = uid;
                    member.Wechat = model.Wechat;
                    int memid = DBService.AddEntity<St_member>(member, true);
                    if (memid > 0)
                    {
                        var claims = new List<Claim>();
                        claims.Add(new Claim(ClaimTypes.Name, model.UserName));
                        claims.Add(new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()));
                        claims.Add(new Claim("http://schemas.microsoft.com/accesscontrolservice/2010/07/claims/identityprovider", "ASP.NET Identity"));
                        claims.Add(new Claim(ClaimTypes.Role, "0"));
                        claims.Add(new Claim("Sex", user.Sex.HasValue && user.Sex.Value ? "1" : "0"));
                        claims.Add(new Claim("DisplayName", user.Name));

                        var identity = new ClaimsIdentity(claims, "MyClaimsLogin");
                        AuthenticationManager.SignIn(new AuthenticationProperties() { IsPersistent = false }, identity);

                        if (!string.IsNullOrEmpty(returnUrl) && Url.IsLocalUrl(returnUrl))
                        {
                            return Redirect(returnUrl);
                        }
                        return RedirectToAction("Index", "Home");
                    }
                    else
                    {
                        ModelState.AddModelError("", "无法注册用户！");
                    }
                }
                else
                {
                    ModelState.AddModelError("", "无法添加用户！");
                }
            }

            return View(model);
        }
    }
}