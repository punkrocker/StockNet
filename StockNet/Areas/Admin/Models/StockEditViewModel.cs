using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using StockNet.Model;

namespace StockNet.Areas.Admin.Models
{
    public class StockEditViewModel
    {
        public St_good Stock { get; set; }
        public IList<St_cat> PCats { get; set; }

        public string name { get; set; }
        public int catid { get; set; }
        public int verifyed { get; set; }
        public int branded { get; set; }
        public int recommended { get; set; }
        public HttpPostedFileBase RecommendPic { get; set; }
        
    }
}