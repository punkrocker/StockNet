using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using StockNet.Model;

namespace StockNet.Areas.Admin.Models
{
    public class RoleIndexViewModel
    {
        public IList<St_role> Roles { get; set; }
    }
}