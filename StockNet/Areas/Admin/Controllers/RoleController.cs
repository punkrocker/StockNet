using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using StockNet.Service;
using StockNet.Areas.Admin.Models;

namespace StockNet.Areas.Admin.Controllers
{
    public class RoleController : ControllerBase
    {
        // GET: Admin/Role
        public ActionResult Index()
        {
            var vm = new RoleIndexViewModel();
            vm.Roles = UserService.GetRoles();
            return View(vm);
        }

        public ActionResult Add()
        {
            return View();
        }

        [HttpPost]
        [ActionName("Add")]
        [ValidateAntiForgeryToken]
        public ActionResult AddPost(string rolename,IList<string> perms)
        {
            UserService.AddRole(rolename, perms);
            return RedirectToAction("Index");
        }

        public ActionResult Edit(int id)
        {
            var vm = new RoleEditViewModel();
            vm.Role = UserService.GetRole(id)
;
            return View(vm);
        }

        [HttpPost]
        [ActionName("Edit")]
        [ValidateAntiForgeryToken]
        public ActionResult EditPost(int id,string rolename, IList<string> perms)
        {
            UserService.EditRole(id,rolename, perms);
            return RedirectToAction("Index");
        }

        [HttpPost]
        [ActionName("Del")]
        [ValidateAntiForgeryToken]
        public JsonResult DelPost(int id)
        {
            UserService.DelRole(id);
            return Json("ok");
        }
    }
}