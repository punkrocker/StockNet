using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace StockNet.Helpers
{
    public static class MenuHelper
    {
        public static string IsActive(this HtmlHelper html, string path)
        {
            return IsActive(html,path,false);
        }
        public static string IsActive(this HtmlHelper html, string path,bool full)
        {
            var noperms = html.ViewContext.TempData["__noperms"];
            if(noperms==null||noperms.ToString()!="1")
            {
                var perms = html.ViewContext.TempData["__perms"];
                string strperms = "";
                if (perms != null)
                {
                    strperms = perms.ToString();
                }
                if(!string.IsNullOrEmpty(strperms))
                {                    
                    string strpath = path;
                    if (strpath.IndexOf(",") > -1)
                    {
                        string[] strccpath = strpath.Split(',');
                        bool scksucc = false;

                        foreach (var sckpath in strccpath)
                        {
                            if (("," + strperms + ",").IndexOf(sckpath, StringComparison.OrdinalIgnoreCase) > -1)
                                scksucc = true;
                        }
                        if (!scksucc)
                        {
                            return "hidden";
                        }
                    }
                    else
                    {
                        if (path.IndexOf("/") > -1)
                            strpath = strpath.Remove(strpath.IndexOf("/"));
                        if (("," + strperms + ",").IndexOf("," + strpath + ",", StringComparison.InvariantCultureIgnoreCase) <= -1)
                        {
                            return "hidden";
                        }
                    }
                }
                else
                {
                    return "hidden";
                }
            }

            string curpath = HttpContext.Current.Request.Url.AbsolutePath;
            if (!curpath.EndsWith("/"))
                curpath += "/";            
            if (curpath.Equals("/Admin/", StringComparison.OrdinalIgnoreCase) && path.Equals("Dash", StringComparison.OrdinalIgnoreCase))
                return "active";
            
            string checkpath = "/Admin/" + path + "/";
            if (path.IndexOf(',') > -1)
            {
                string[] paths = path.Split(',');
                bool cksucc = false;

                foreach (var ckpath in paths)
                {
                    if (!full && curpath.IndexOf(ckpath, StringComparison.OrdinalIgnoreCase) > -1)
                        cksucc = true;
                    if (full && curpath.Equals(ckpath, StringComparison.OrdinalIgnoreCase))
                        cksucc = true;
                }
                if (cksucc)
                {
                    return "active";
                }
            }
            if (!full && curpath.IndexOf(checkpath, StringComparison.OrdinalIgnoreCase) > -1)
                return "active";
            if (full && curpath.Equals(checkpath, StringComparison.OrdinalIgnoreCase))
                return "active";
            return null;
        }
    }
}