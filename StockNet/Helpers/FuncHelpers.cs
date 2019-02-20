using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using StockNet.Service;

namespace StockNet.Helpers
{
    public static class FuncHelpers
    {
        public static IEnumerable<int> Range(int min, int max)
        {
            for (int i = min; i <= max; i++)
            {
                yield return i;
            }
        }

        public static string GetFooterInfo()
        {
            var st = SettingService.GetSetting("st_footerinfo");
            if (st != null && st.Id > 0)
            {
                var strvv = st.Val;
                if (!string.IsNullOrEmpty(strvv))
                {
                    strvv ="<p>"+ strvv.Replace(Environment.NewLine, "</p><p>")+"</p>";
                }
                return strvv;
            }
            else
                return "";
        }

        public static string GetMarketInfo()
        {
            var st = SettingService.GetSetting("st_marketinfo");
            if (st != null && st.Id > 0)
            {
                var strvv = st.Val;
                if (!string.IsNullOrEmpty(strvv))
                {
                    strvv = "<strong class=\"r\">" + strvv.Replace(Environment.NewLine, "</strong><strong class=\"r\">") + "</strong>";
                }
                return strvv;
            }
            else
                return "";
        }

        public static string GetItemIndexPagedUrl(int p)
        {
            return GetPagedUrl("/item", p);
        }
        public static string GetItemBuyPagedUrl(int p)
        {
            return GetPagedUrl("/item/buy", p);
        }
        public static string GetItemBrandPagedUrl(int p)
        {
            return GetPagedUrl("/item/brand", p);
        }
        public static string GetManageStocksPagedUrl(int p)
        {
            return GetPagedUrl("/manage/stocks", p);
        }
        private static string GetPagedUrl(string path,int p)
        {
            string url = path+"?p="+p;
            string k = HttpContext.Current.Request.QueryString["k"];
            string cat = HttpContext.Current.Request.QueryString["cat"];
            if (!string.IsNullOrEmpty(k))
                url += "&k="+k;
            if (!string.IsNullOrEmpty(cat))
                url += "&cat=" + cat;
            return url;
        }
    }
}