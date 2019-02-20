using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using StockNet.Service;
using StockNet.Models;
using StockNet.Model;
using System.Web.Caching;

namespace StockNet.Controllers
{
    public class HomeController : Controller
    {        
        public ActionResult Index()
        {
            var vm = new HomeIndexViewModel();
            var vmcache = GetCache();
            if (vmcache != null && vmcache is HomeIndexViewModel)
                vm = vmcache as HomeIndexViewModel;
            else
            {
                vm.TopLevelCats = StockService.GetTopLevelStockCats();
                vm.HotStocks = StockService.GetHotStocks(10);
                vm.LatestStocks = StockService.GetlatestStocks(40);
                if (vm.LatestStocks != null && vm.LatestStocks.Count > 0)
                {
                    var ids = new List<int>();
                    foreach (var lst in vm.LatestStocks)
                        if (lst.CatId.HasValue && lst.CatId.Value > 0 && !ids.Contains(lst.CatId.Value))
                            ids.Add(lst.CatId.Value);
                    if (ids.Count > 0)
                    {
                        vm.LatestStocksCats = StockService.GetlatestStocksCats(ids);
                    }
                }
                vm.RecommendStocks = StockService.GetRecommendStocks(5);
                vm.LatestNotices = NoticeService.GetLatestNotices(5, 0);

                AddToCache(vm);
            }
            return View(vm);
        }
        private const string _indexcachekey = "_indexcachekey";
        private void AddToCache(object obj)
        {
            HttpContext.Cache.Add(_indexcachekey,obj,null,Cache.NoAbsoluteExpiration,new TimeSpan(0,15,0),CacheItemPriority.Normal,null);
        }

        private object GetCache()
        {
            var obj = HttpContext.Cache.Get(_indexcachekey);
            return obj;
        }

        public ActionResult Contact()
        {
            ViewBag.InfoContent = SettingService.GetSettingContent("st_contact");
            return View();
        }

        public ActionResult About()
        {
            ViewBag.InfoContent = SettingService.GetSettingContent("st_about");
            return View();
        }

        public ActionResult Terms()
        {
            ViewBag.InfoContent = SettingService.GetSettingContent("st_terms");
            return View();
        }

        public ActionResult Disclaimer()
        {
            ViewBag.InfoContent = SettingService.GetSettingContent("st_disclaimer");
            return View();
        }

        public ActionResult Notice(int? id)
        {
            var vm = new NoticeListViewModel();
            if (id.HasValue && id.Value > 0)
            {
                vm.Notices = new List<St_notice>();
                var nt = DBService.GetEntity<St_notice>(id.Value);
                if (nt != null && nt.Id > 0)
                {
                    vm.IsOut = nt.NoticeType == 1;
                    vm.Notices.Add(nt);
                }
            }
            else
            {
                vm.Notices = NoticeService.GetLatestNotices(20,0);
            }
            return View(vm);
        }

        public ActionResult NoticeB(int? id)
        {
            var vm = new NoticeListViewModel();
            if (id.HasValue && id.Value > 0)
            {
                vm.Notices = new List<St_notice>();
                var nt = DBService.GetEntity<St_notice>(id.Value);
                if (nt != null && nt.Id > 0)
                {
                    vm.IsOut = nt.NoticeType == 1;
                    vm.Notices.Add(nt);
                }
            }
            else
            {
                vm.Notices = NoticeService.GetLatestNotices(20, 1);
                vm.IsOut = true;
            }
            return View("Notice", vm);
        }
    }
}