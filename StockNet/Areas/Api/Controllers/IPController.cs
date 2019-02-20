using Newtonsoft.Json;
using StockNet.Model;
using StockNet.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Mvc;

namespace StockNet.Areas.Api.Controllers
{
    public class IPController : Controller 
    {
        // GET api/ip/5
        public ActionResult Get()
        {
            IP ip = new IP();
            ip.IPAddr = SettingService.GetSettingContent("st_ip");
            return Content(JsonConvert.SerializeObject(ip));
        }

        
        // POST api/ip
        public ActionResult Set(string newIP)
        {
            SettingService.SetSettingContent("st_ip", newIP);
            return Content("aaa");
        }
    }

    public class IP
    {
        public string IPAddr
        {
            get;
            set;
        }
    }
}
