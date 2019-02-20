using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using StockNet.Model;

namespace StockNet.Areas.Api.Models
{
    public class ItemDetail
    {
        public bool IsVip { get; set; }

        public St_good Stock { get; set; }
        public IList<St_pic> StockPics { get; set; }
    }
}