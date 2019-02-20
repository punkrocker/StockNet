using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using StockNet.Model;

namespace StockNet.Models
{
    public class ItemListViewModel
    {
        public IList<St_cat> TopLevelCats { get; set; }
        public IList<St_cat> SubLevelCats { get; set; }
        public IList<St_good> HotStocks { get; set; }
        public int FirstCatLevel { get; set; }
        public IList<KeyValuePair<int, string>> CatPath { get; set; }
        public int CatID { get; set; }
        public string CatName { get; set; }

        public IList<St_good> CurPageStocks { get; set; }
        public int CurPageIndex { get; set; }
        public int PageSize { get; set; }
        public int PageTotal { get; set; }
        public int RecordCount { get; set; }
    }
}