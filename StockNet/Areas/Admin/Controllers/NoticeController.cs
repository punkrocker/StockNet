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
    public class NoticeController : ControllerBase
    {
        // GET: Admin/Notice
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public JsonResult List(PagingRequest pg)
        {
            var res = new PagingReply();
            res.iTotalRecords = NoticeService.GetNoticeCount(pg.sSearch);
            if (res.iTotalRecords > 0)
            {
                var entitys = NoticeService.GetNotices(pg.iDisplayStart, pg.iDisplayLength, pg.sSearch);
                res.aaData = entitys.Select(ct => new object[] { ct.Title,ct.NoticeType==1?"曝光台":"网站公告", ct.AddTime.ToString(), ct.Id }).ToList();
            }
            res.iTotalDisplayRecords = res.iTotalRecords;
            res.sEcho = pg.sEcho.ToString();

            return Json(res);
        }

        public ActionResult Add()
        {
            return View();
        }

        [HttpPost]
        [ActionName("Add")]
        [ValidateAntiForgeryToken]
        [ValidateInput(false)]
        public ActionResult AddPost(string title, string notice,int typeid)
        {
            if (!string.IsNullOrEmpty(title) && !string.IsNullOrEmpty(notice))
            {
                var note = new St_notice();
                note.AddTime = DateTime.Now;
                note.AddUser = GetCurUserId();
                note.Notice = notice;
                note.Title = title;
                note.NoticeType = typeid;
                DBService.AddEntity<St_notice>(note);
            }
            return RedirectToAction("Index");
        }

        public ActionResult Edit(int id)
        {
            var vm = new NoticeEditViewModel();
            vm.Notice = DBService.GetEntity<St_notice>(id);
            return View(vm);
        }

        [HttpPost]
        [ActionName("Edit")]
        [ValidateAntiForgeryToken]
        [ValidateInput(false)]
        public ActionResult EditPost(int id, string title, string notice)
        {
            if (id>0 &&!string.IsNullOrEmpty(title) && !string.IsNullOrEmpty(notice))
            {
                var note = DBService.GetEntity<St_notice>(id); 
                note.Notice = notice;
                note.Title = title;
                DBService.UpdateEntity<St_notice>(note);
            }
            return RedirectToAction("Index");
        }

        [HttpPost]
        [ActionName("Del")]
        [ValidateAntiForgeryToken]
        public JsonResult DelPost(int id)
        {
            DBService.DeleteEntity<St_notice>(id);
            return Json("ok");
        }
    }
}