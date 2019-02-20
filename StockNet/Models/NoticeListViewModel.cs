using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using StockNet.Model;

namespace StockNet.Models
{
    public class NoticeListViewModel
    {
        public IList<St_notice> Notices { get; set; }

        public bool IsOut { get; set; }
    }
}