using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using StockNet.Service;
using StockNet.Model;
using StockNet.Areas.Admin.Models;

namespace StockNet.Areas.Admin.Controllers
{
    public class SettingController : ControllerBase
    {
        // GET: Admin/Setting
        public ActionResult Index()
        {
            var vm = new SettingIndexViewModel();
            vm.Settings = SettingService.GetSettings();
            return View(vm);
        }

        [HttpPost]
        [ActionName("Index")]
        [ValidateAntiForgeryToken]
        public ActionResult IndexPOST()
        {
            var formvals =new Dictionary<string, string>();
            foreach(var key in Request.Form.AllKeys)
            {
                if (key.StartsWith("st_"))
                {
                    formvals.Add(key, Request.Form[key]);
                }
            }
            if (formvals.Count > 0)
            {
                SettingService.SaveSettings(formvals);
                Session["__settings"] = null;
            }
            return RedirectToAction("Index");
        }

        public ActionResult Item(string k,string n)
        {
            var vm = new SettingItemViewModel();
            vm.Key = k;
            vm.Name = n;
            if (!string.IsNullOrEmpty(k))
                vm.Setting = SettingService.GetSetting("st_"+k);
            return View(vm);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [ActionName("Item")]
        [ValidateInput(false)]
        public ActionResult ItemPOST(string k,string item)
        {
            if (!string.IsNullOrEmpty(k))
            {
                var st = SettingService.GetSetting("st_" + k);
                if (st != null)
                {
                    st.Val = item;
                    DBService.UpdateEntity<St_setting>(st);
                }
                else
                {
                    st = new St_setting();
                    st.AddTime = DateTime.Now;
                    st.Name = "st_" + k;
                    st.Val = item;
                    DBService.AddEntity<St_setting>(st);
                }
            }
            return RedirectToAction("Index");
        }
    }
}