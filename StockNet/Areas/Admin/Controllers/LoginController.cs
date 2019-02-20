using Microsoft.Owin.Security;
using StockNet.Models;
using StockNet.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Web;
using System.Web.Mvc;

namespace StockNet.Areas.Admin.Controllers
{
    public class LoginController : Controller
    {
        // GET: Admin/Login
        public ActionResult Index(string returnUrl)
        {
            ViewBag.ReturnUrl = returnUrl;
            return View();
        }

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public ActionResult Check(LoginViewModel model, string returnUrl)
        {
            if (!ModelState.IsValid)
            {
                return View("Index",model);
            }

            LogService.AdminLogin(model.UserName+";"+Request.UserHostAddress+";"+Request.UserAgent);

            var identity = UserService.CheckIdentity(model.UserName, model.Password,true);
            if (identity != null)
            {
                //AuthenticationManager.SignOut();
                AuthenticationManager.SignIn(new AuthenticationProperties() { IsPersistent = model.RememberMe=="on" }, identity);
                return RedirectToLocal(returnUrl);
            }
            else
            {
                ModelState.AddModelError("", "无效的登录尝试。");
                return View("Index", model);
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult LogOff()
        {
            string struid = "",curtoken="";
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
            UserService.UpdateLoginToken(uid,curtoken, "");

            AuthenticationManager.SignOut();            
            return Redirect("/Admin/Login");
        }

        private IAuthenticationManager AuthenticationManager
        {
            get { return HttpContext.GetOwinContext().Authentication; }
        }

        private ActionResult RedirectToLocal(string returnUrl)
        {
            if (Url.IsLocalUrl(returnUrl))
            {
                return Redirect(returnUrl);
            }
            return Redirect("/Admin/Dash");
        }
    }
}