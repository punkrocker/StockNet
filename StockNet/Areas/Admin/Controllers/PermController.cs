using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace StockNet.Areas.Admin.Controllers
{
    [Authorize]
    public class PermController : Controller
    {
        // GET: Admin/Perm
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Out()
        {
            return View();
        }
    }
}