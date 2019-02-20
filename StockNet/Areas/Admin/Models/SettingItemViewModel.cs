using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using StockNet.Model;

namespace StockNet.Areas.Admin.Models
{
    public class SettingItemViewModel
    {
        public St_setting Setting { get; set; }

        public string Key { get; set; }
        public string Name { get; set; }
    }
}