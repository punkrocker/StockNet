using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using StockNet.Models;

namespace StockNet.Areas.Api.Models
{
    public class PublishItem : ItemPublishViewModel
    {
        public string MainPicName { get; set; }
        public int uid { get; set; }
    }
}