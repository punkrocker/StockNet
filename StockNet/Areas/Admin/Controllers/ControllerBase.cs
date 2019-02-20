using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Security.Principal;
using System.Security.Claims;
using StockNet.Model;
using StockNet.Service;

namespace StockNet.Areas.Admin.Controllers
{
    [Authorize(Roles = "1,-1")]
    [PermFilter]
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
    }
}