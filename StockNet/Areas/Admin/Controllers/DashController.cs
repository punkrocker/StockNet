using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using StockNet.Service;
using StockNet.Areas.Admin.Models;
using StockNet.Models;
using StockNet.Model;

namespace StockNet.Areas.Admin.Controllers
{
    public class DashController : ControllerBase
    {
        // GET: Admin/Dash
        public ActionResult Index()
        {
            var vm = new DashIndexViewModel();
            vm.StockLatest = DashService.GetLatestStocks();
            CountDTO cdto = DashService.GetStockMemberCount();
            vm.StockTodayCount = cdto.StockTodayCount;
            vm.StockMonthCount = cdto.StockMonthCount;
            vm.MemberTodayCount = cdto.MemberTodayCount;
            vm.MemberMonthCount = cdto.MemberMonthCount;
            vm.StockMonthlyCount = DashService.GetStockMonthlyCount();
            vm.MemberMonthlyCount = DashService.GetMemberMonthlyCount();

            return View(vm);
        }
    }
}