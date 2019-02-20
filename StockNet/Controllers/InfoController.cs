using StockNet.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace StockNet.Controllers
{
    public class InfoController : Controller
    {
        // GET: Info
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Guide()
        {
            ViewBag.InfoContent = SettingService.GetSettingContent("st_guide");
            return View();
        }

        public ActionResult Agent()
        {
            ViewBag.InfoContent = SettingService.GetSettingContent("st_agent");
            return View();
        }
    }
}