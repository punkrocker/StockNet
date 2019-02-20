using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using StockNet.Model;
using StockNet.Service;

namespace StockNet.Helpers
{
    public static class MyHtmlHelper
    {
        //public static Settings GetSettings(this HtmlHelper html)
        //{
        //    Settings _settings = null;
            
        //    object sessionsettings = HttpContext.Current.Session["__settings"];
        //    if (sessionsettings == null || !(sessionsettings is Settings))
        //    {
        //        _settings = SettingService.GetSettingsObject();
        //        HttpContext.Current.Session["__settings"] = _settings;
        //    }
        //    else
        //        _settings = sessionsettings as Settings;
            
        //    if (_settings == null)
        //        return new Settings();
        //    else
        //        return _settings;
        //}

        public static IHtmlString GetCatPath(this HtmlHelper html,string path)
        {
            var rpath = "";
            if (!string.IsNullOrEmpty(path))
            {
                var apath = path.Split(',');
                for (int i = 0; i < apath.Length; i++)
                {
                    if (i >= 3) {
                        if (i == apath.Length - 1)
                        {
                            rpath += "┕";
                        }
                        else {
                            rpath += "&nbsp;&nbsp;";
                        }
                    }
                }
            }

            return html.Raw(rpath);
        }

        public static string GetWords(this HtmlHelper html, string words,int maxWords)
        {
            var wds = "";
            if (!string.IsNullOrEmpty(words) && maxWords > 0 && maxWords<words.Length)
            {
                wds = words.Remove(maxWords) + "...";
            }
            else
                wds = words;

            return wds;
        }
    }
}