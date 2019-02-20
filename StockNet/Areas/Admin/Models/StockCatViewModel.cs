using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using StockNet.Model;

namespace StockNet.Areas.Admin.Models
{
    public class StockCatViewModel
    {
        public IList<St_cat> StockCats { get; set; }
    }
}