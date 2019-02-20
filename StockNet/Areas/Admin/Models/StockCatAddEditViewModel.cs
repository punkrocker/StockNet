using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using StockNet.Model;

namespace StockNet.Areas.Admin.Models
{
    public class StockCatAddEditViewModel
    {
        public IList<St_cat> PCats { get; set; }
        public St_cat Cat { get; set; }
    }
}