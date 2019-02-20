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
    public class UserController : ControllerBase
    {
        // GET: Admin/User
        public ActionResult Index()
        {
            var vm = new UserIndexViewModel();
            vm.Users = UserService.GetAdminUsers();
            vm.Roles = UserService.GetRoles();

            return View(vm);
        }

        public ActionResult Add()
        {
            var vm = new UserAddEditViewModel();
            vm.Roles = UserService.GetRoles();

            return View(vm);
        }

        [HttpPost]
        [ActionName("Add")]
        [ValidateAntiForgeryToken]
        public ActionResult AddPost(UserAddEditViewModel model)
        {
            if (model.username !=null && model.username.Equals("admin", StringComparison.OrdinalIgnoreCase))
            {
                ModelState.AddModelError("username", "用户名不能为admin");
            }
            if (!ModelState.IsValid)
            {
                model.Roles = UserService.GetRoles();
                return View(model);
            }
            St_user u = new St_user();
            u.LoginName = model.username;
            u.LoginPass = DBService.SHA1Hash(model.userpass);
            u.Name = model.realname;
            u.Email = model.email;
            u.Sex = model.usersex == 1 ? true : false;
            u.RoleId = model.userrole>0?model.userrole:0;
            u.AddTime = DateTime.Now;
            u.Status = 0;
            DBService.AddEntity<St_user>(u);

            return RedirectToAction("Index");
        }

        public ActionResult Edit(int id)
        {
            var vm = new UserAddEditViewModel();
            vm.Roles = UserService.GetRoles();
            vm.User = UserService.GetUser(id);

            return View(vm);
        }

        [HttpPost]
        [ActionName("Edit")]
        [ValidateAntiForgeryToken]
        public ActionResult EditPost(int id,UserAddEditViewModel model)
        {
            St_user u = DBService.GetEntity<St_user>(id);
            if (!u.LoginName.Equals("admin", StringComparison.OrdinalIgnoreCase) && model.username != null && model.username.Equals("admin", StringComparison.OrdinalIgnoreCase))
            {
                ModelState.AddModelError("username", "用户名不能为admin");
            }

            if (!ModelState.IsValid)
            {
                model.Roles = UserService.GetRoles();
                return View(model);
            }

            if (!u.LoginName.Equals("admin", StringComparison.OrdinalIgnoreCase))
            {
                u.LoginName = model.username;
                u.RoleId = model.userrole > 0 ? model.userrole : 0;
            }
            if(!string.IsNullOrEmpty(model.userpass))
                u.LoginPass = DBService.SHA1Hash(model.userpass);
            u.Name = model.realname;
            u.Email = model.email;
            u.Sex = model.usersex == 1 ? true : false;            
            if (u.AddTime == null)
                u.AddTime = DateTime.Now;
            DBService.UpdateEntity<St_user>(u);

            return RedirectToAction("Index");
        }

        [CurUserPermFilter]
        public ActionResult ChangePassword()
        {
            return View();
        }

        [HttpPost]
        [ActionName("ChangePassword")]
        [ValidateAntiForgeryToken]
        [CurUserPermFilter]
        public ActionResult ChangePasswordPOST(ChangePasswordViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }
            int res = UserService.ChangeUserPass(GetCurUserId(),model.OldPassword, model.NewPassword);
            if (res == 0)
                TempData["__message"] = "修改密码成功！";
            else
                TempData["__message"] = "当前密码不正确！";
            return RedirectToAction("ChangePassword");
        }

        [HttpPost]
        [ActionName("Del")]
        [ValidateAntiForgeryToken]
        public JsonResult DelPost(int id)
        {
            UserService.DelUser(id);
            return Json("ok");
        }
    }
}