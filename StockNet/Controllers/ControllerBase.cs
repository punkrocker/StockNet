using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Security.Principal;
using System.Security.Claims;
using StockNet.Model;
using StockNet.Service;

namespace StockNet.Controllers
{
    public class ControllerBase:Controller
    {
        protected int GetCurUserId()
        {
            string struid = "";
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
                }
            }
            return uid;
        }

        protected ActionResult RedirectToLocal(string returnUrl)
        {
            if (Url.IsLocalUrl(returnUrl))
            {
                return Redirect(returnUrl);
            }
            return RedirectToAction("Index", "Home");
        }
    }
}