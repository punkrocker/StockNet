using StockNet.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Web;
using System.Web.Mvc;

namespace StockNet
{
    public class TokenFilterAttribute : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            var checkpass = false;
            if (filterContext.HttpContext.Request.IsAuthenticated)
            {
                string uid = "";
                var uuid = 0;
                var ci = filterContext.HttpContext.User.Identity as ClaimsIdentity;
                if (ci != null)
                {
                    var token = "";
                    var tokenclaim = ci.Claims.SingleOrDefault(cl => cl.Type == "Token");
                    if (tokenclaim != null)
                    {
                        token = tokenclaim.Value;
                    }
                    var claim = ci.Claims.SingleOrDefault(cl => cl.Type == ClaimTypes.NameIdentifier);
                    if (claim != null)
                    {
                        uid = claim.Value;

                        int.TryParse(uid, out uuid);
                    }
                    var dbtoken = UserService.GetToken(uuid);
                    if (token == dbtoken)
                    {
                        checkpass = true;
                    }
                }
            }
            if (!checkpass)
            {
                filterContext.Result = new RedirectResult("/Account/Out");
            }
            base.OnActionExecuting(filterContext);
        }
    }
}