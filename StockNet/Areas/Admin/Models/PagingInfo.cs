using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace StockNet.Areas.Admin.Models
{
    public class PagingRequest
    {
        public int iDisplayStart { get; set; }
        public int iDisplayLength { get; set; }
        //public int iColumns { get; set; }
        public string sSearch { get; set; }
        public int sEcho { get; set; }
    }
    public class PagingReply
    {
        public PagingReply()
        {
            aaData = new List<object[]>();
        }
        public int iTotalRecords { get; set; }
        public int iTotalDisplayRecords { get; set; }
        public string sEcho { get; set; }
        public IList<object[]> aaData { get; set; }
    }
}