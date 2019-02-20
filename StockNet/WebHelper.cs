using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Text.RegularExpressions;

namespace StockNet
{
    public static class WebHelper
    {
        public static string StripHtml(string htmlContent)
        {
            string res = "";
            if (!string.IsNullOrEmpty(htmlContent))
            {
                Regex regex = new Regex(@"<[^>]+>|</[^>]+>");
                res = regex.Replace(htmlContent, "");
                res = HttpContext.Current.Server.HtmlEncode(res);
            }
            return res;
        }
    }
}