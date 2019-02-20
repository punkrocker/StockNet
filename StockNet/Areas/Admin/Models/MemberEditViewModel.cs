using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using StockNet.Model;

namespace StockNet.Areas.Admin.Models
{
    public class MemberEditViewModel
    {
        public St_member Member { get; set; }
        public St_user User { get; set; }

        public string username { get; set; }
        public string level { get; set; }
        public int verifyed {get;set;}
        public int userlock { get; set; }
    }
}