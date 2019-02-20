using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using StockNet.Service;
using StockNet.Areas.Admin.Models;
using StockNet.Models;
using StockNet.Model;
using System.IO;

namespace StockNet.Areas.Admin.Controllers
{
    public class StockController : ControllerBase
    {
        // GET: Admin/Stock
        public ActionResult Index(int? catid, int? memberid, string q)
        {
            var vm = new StockIndexViewModel();
            if (memberid > 0)
                vm.Member = DBService.GetEntity<St_member>(memberid.Value);
            vm.MemberId = memberid;
            vm.Search = q;
            return View(vm);
        }

        [HttpPost]
        public JsonResult List(int? memberid ,string q , PagingRequest pg)
        {
            var res = new PagingReply();
            string qr = q;
            if (!string.IsNullOrEmpty(pg.sSearch))
                qr = pg.sSearch;
            res.iTotalRecords = StockService.GetStockCount(memberid, qr);
            if (res.iTotalRecords > 0)
            {
                var cats = StockService.GetStockCats();

                var results = StockService.GetStocks(memberid, pg.iDisplayStart, pg.iDisplayLength, qr);
                var stockmemberids = results.Where(s=>s.MemberId>0).Select(s => s.MemberId.HasValue ? s.MemberId.Value : 0).ToList();
                var members = MemberService.GetMembersByIds(stockmemberids);
                res.aaData = results.Select(ct => new object[] { ct.Name, 
                    ct.CatId.HasValue && ct.CatId > 0 ? GetCatName(ct.CatId.Value,cats) : "无", 
                    !string.IsNullOrEmpty(ct.MainPic) && ct.MainPic.StartsWith("~") ? VirtualPathUtility.ToAbsolute(ct.MainPic) : ct.MainPic, 
                    "区域："+ct.GoodsArea+" 类型："+(ct.GoodsType==1?"<b>求购</b>":"<b>出售</b>")+"<br>"+(ct.IsBrandGoods==true?"品牌："+ct.BrandName+"<br>":"")+ "价格：" + (ct.Price.HasValue ? ct.Price.Value.ToString() + " " : " ") + ct.PriceDetail + " 数量：" + (ct.Qty.HasValue ? ct.Qty.Value.ToString() + " " : " ") + ct.QtyDetail,
                    ct.Status == 1 ? "已验证" : "未验证", ct.AddTime.ToString()+"<br>会员名："+GetMemberName(ct.MemberId,members), ct.Id }).ToList();
            }
            res.iTotalDisplayRecords = res.iTotalRecords;
            res.sEcho = pg.sEcho.ToString();

            return Json(res);
        }

        private string GetCatName(int catid,IList<St_cat> cats)
        {
            string res = "无";
            if(catid>0&&cats!=null&&cats.Count>0)
            {
                foreach (var cat in cats)
                    if (cat.Id == catid)
                    {
                        res = cat.Name;
                        break;
                    }
            }
            return res;
        }


        private string GetMemberName(int? memberID, IList<St_member> members)
        {
            var membername = "";
            if (memberID.HasValue && memberID.Value > 0 && members != null && members.Count > 0)
            {
                membername = members.Where(s => s.Id == memberID.Value).Select(s => s.RealName).SingleOrDefault();
            }
            return membername;
        }

        public ActionResult Edit(int id)
        {
            var vm = new StockEditViewModel();
            vm.Stock = DBService.GetEntity<St_good>(id);
            vm.PCats = StockService.GetStockCats();
            return View(vm);
        }

        [HttpPost]
        [ActionName("Edit")]
        [ValidateAntiForgeryToken]
        public ActionResult EditPOST(int id, StockEditViewModel model)
        {
            if (ModelState.IsValid)
            {
                var good = DBService.GetEntity<St_good>(id);
                good.Name = model.name;
                good.CatId = model.catid;
                if (model.catid > 0)
                {
                    var ccat = DBService.GetEntity<St_cat>(model.catid);
                    if (ccat != null && ccat.Id > 0)
                        good.CatPath = ccat.Path;
                }
                good.Status = model.verifyed;
                if(string.IsNullOrEmpty(good.SerialNum))
                    good.SerialNum = "WK" + DateTime.Now.ToString("yyMMdd") + good.Id;
                good.Recommended = model.recommended == 1;
                good.IsBrandGoods = model.branded == 1;

                if (good.Recommended==true  &&model.RecommendPic != null && model.RecommendPic.ContentLength > 0)
                {
                    try
                    {
                        DateTime now = DateTime.Now;
                        string vpath = "/Site/Goods/" + now.ToString("yyyyMMdd") + "/";
                        string realpath = Server.MapPath(vpath);
                        if (!Directory.Exists(realpath))
                            Directory.CreateDirectory(realpath);
                        string filename = model.RecommendPic.FileName;
                        if (System.IO.File.Exists(realpath + filename))
                        {
                            filename = now.Ticks.ToString() + "_" + filename;
                        }
                        model.RecommendPic.SaveAs(realpath + filename);
                        good.RecommendPic = vpath + filename;
                    }
                    catch
                    {

                    }
                }

                DBService.UpdateEntity<St_good>(good);
                return RedirectToAction("Index");
            }
            return View(model);
        }

        [HttpPost]
        [ActionName("Del")]
        [ValidateAntiForgeryToken]
        public JsonResult DelPost(int id)
        {
            DBService.DeleteEntity<St_good>(id);
            return Json("ok");
        }

        public ActionResult Cat()
        {
            var vm = new StockCatViewModel();
            vm.StockCats = StockService.GetStockCats();

            return View(vm);
        }

        public ActionResult CatAdd()
        {
            var vm = new StockCatAddEditViewModel();
            vm.PCats = StockService.GetStockCats();

            return View(vm);
        }

        [HttpPost]
        [ActionName("CatAdd")]
        [ValidateAntiForgeryToken]
        public ActionResult CatAddPost(string name,int pid)
        {
            if (!string.IsNullOrEmpty(name))
            {
                St_cat aCat = new St_cat();
                aCat.Name = name;
                aCat.PId = pid;
                aCat.Level = 0;
                int lid = DBService.AddEntity<St_cat>(aCat,true);
                string catpath = "";
                if (pid > 0)
                {
                    var pcat = DBService.GetEntity<St_cat>(pid);
                    if (pcat != null && pcat.Id > 0)
                    {
                        catpath = string.IsNullOrEmpty(pcat.Path) ? "," : pcat.Path + lid.ToString() + ",";
                    }
                }
                else
                {
                    catpath="," + lid.ToString() + ",";
                }

                //aCat.Path = catpath;
                StockService.UpdateCatPath(lid,catpath);
            }
            return RedirectToAction("Cat");
        }

        public ActionResult CatEdit(int id)
        {
            var vm = new StockCatAddEditViewModel();
            vm.Cat = DBService.GetEntity<St_cat>(id);
            vm.PCats = StockService.GetStockCats();

            return View(vm);
        }

        [HttpPost]
        [ActionName("CatEdit")]
        [ValidateAntiForgeryToken]
        public ActionResult CatEditPost(int id, string name, int pid)
        {
            if (id > 0)
            {
                St_cat aCat = DBService.GetEntity<St_cat>(id);
                aCat.Name = name;
                string oldpath = aCat.Path;
                if (aCat.PId != pid && pid > 0)
                {
                    var pcat = DBService.GetEntity<St_cat>(pid);
                    if (pcat != null && pcat.Id > 0 && !pcat.Path.StartsWith(aCat.Path))
                    {
                        aCat.PId = pid;
                        aCat.Path = string.IsNullOrEmpty(pcat.Path) ? "," : pcat.Path + id.ToString() + ",";
                        var pa = aCat.Path.Split(',');
                        if (pa.Length > 3)
                            aCat.Level = pa.Length - 3;
                        else
                            aCat.Level = 0;

                        StockService.UpdateSubCatPath(id, oldpath, aCat.Path);
                    }                    
                }
                DBService.UpdateEntity<St_cat>(aCat);
                StockService.UpdateCatPath(id, aCat.Path);
            }
            return RedirectToAction("Cat");
        }

        [HttpPost]
        [ActionName("CatDel")]
        [ValidateAntiForgeryToken]
        public JsonResult CatDelPost(int id)
        {
            StockService.DeleteCat(id);
            return Json("ok");
        }
    }
}