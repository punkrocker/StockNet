using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using StockNet.Model;

namespace StockNet.Models
{
    public class ItemShowViewModel
    {
        public IList<St_cat> TopLevelCats { get; set; }
        public IList<St_cat> SubLevelCats { get; set; }
        public IList<St_good> HotStocks { get; set; }
        public int FirstCatLevel { get; set; }

        public St_member Member { get; set; }
        public bool IsVip { get; set; }

        public St_good Stock { get; set; }
        public IList<St_pic> StockPics { get; set; }
        public int CatID { get; set; }
        public string CatName { get; set; }
        public IList<KeyValuePair<int, string>> CatPath { get; set; }
    }
}