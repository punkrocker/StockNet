using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Security.Principal;
using System.Security.Claims;
using StockNet.Service;

namespace StockNet.Areas.Admin.Controllers
{
    public class PermFilterAttribute:ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            var checkpass = false;
            var notpasstipurl = "/Admin/Perm";
            if(filterContext.HttpContext.Request.IsAuthenticated)
            {
                string uid = "",rid="";
                var uuid = 0;
                var ci = filterContext.HttpContext.User.Identity as ClaimsIdentity;
                if (ci != null)
                {
                    var rclaim = ci.Claims.SingleOrDefault(cl => cl.Type == ClaimTypes.Role);
                    if (rclaim != null)
                        rid = rclaim.Value;
                    if (rid == "-1"||rid=="1")
                    {
                        var token = "";                        
                        var tokenclaim = ci.Claims.SingleOrDefault(cl => cl.Type == "Token");
                        if(tokenclaim!=null)
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
                        if(token==dbtoken)
                        {
                            if (rid == "-1")
                            {
                                checkpass = true;
                                filterContext.Controller.TempData["__noperms"] = 1;
                            }
                            else if (rid == "1")
                            {
                                var perms = UserService.GetUserPerms(uuid);
                                filterContext.Controller.TempData["__perms"] = perms;
                                if (!string.IsNullOrEmpty(perms) && ("," + perms + ",").IndexOf("," + filterContext.ActionDescriptor.ControllerDescriptor.ControllerName + ",", StringComparison.InvariantCultureIgnoreCase) > -1)
                                    checkpass = true;
                                if (!checkpass)
                                {
                                    var filters = filterContext.ActionDescriptor.GetFilterAttributes(true);
                                    bool iscurfilter = false;
                                    foreach (var filte in filters)
                                    {
                                        if (filte.GetType() == typeof(CurUserPermFilterAttribute))
                                            iscurfilter = true;
                                    }
                                    if (iscurfilter)
                                        checkpass = true;
                                }
                            }
                        }
                        else
                            notpasstipurl = "/Admin/Perm/Out";
                    }                    
                }
            }
            if(!checkpass)
            {
                filterContext.Result = new RedirectResult(notpasstipurl);
            }
            base.OnActionExecuting(filterContext);
        }
    }

    public class CurUserPermFilterAttribute:FilterAttribute
    {
    }
}