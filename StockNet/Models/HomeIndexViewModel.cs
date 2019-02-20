using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using StockNet.Model;

namespace StockNet.Models
{
    public class HomeIndexViewModel
    {
        public IList<St_cat> TopLevelCats { get; set; }
        public IList<St_good> HotStocks { get; set; }
        public IList<St_notice> LatestNotices { get; set; }
        public IList<St_good> RecommendStocks { get; set; }
        public IList<St_good> LatestStocks { get; set; }
        public IList<St_cat> LatestStocksCats { get; set; }
    }
}