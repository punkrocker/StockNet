using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using StockNet.Models;
using StockNet.Model;
using StockNet.Service;
using System.IO;

namespace StockNet.Controllers
{
    public class ItemController : ControllerBase
    {
        // GET: List
        public ActionResult Index(int? p,int? cat,string k,string area)
        {
            var vm = new ItemListViewModel();
            vm.FirstCatLevel = 0;
            if(cat.HasValue&&cat.Value>0)
            {
                vm.SubLevelCats = StockService.GetSubLevelStockCats(cat.Value);
                //if(vm.TopLevelCats==null||vm.TopLevelCats.Count<1)
                //    vm.TopLevelCats = StockService.GetTopLevelStockCats();
                //else
                //{
                    var pcat = DBService.GetEntity<St_cat>(cat.Value);
                    if (pcat != null && pcat.Id > 0)
                    {
                        vm.FirstCatLevel = pcat.Level.HasValue ? pcat.Level.Value + 1 : 0;
                        if (!string.IsNullOrEmpty(pcat.Path))
                        {
                            vm.CatPath = StockService.GetStockCatPath(pcat.Path);
                        }
                        vm.CatID = cat.Value;
                        vm.CatName = pcat.Name;
                    }
                //}
            }
            vm.TopLevelCats = StockService.GetTopLevelStockCats();

            vm.HotStocks = StockService.GetHotStocks(10);

            var pgnum = 40;
            vm.PageSize = pgnum;
            vm.CurPageIndex = p.HasValue && p.Value > 1 ? p.Value : 1;            
            vm.RecordCount = StockService.GetTotalStockCount(cat,k);
            vm.PageTotal = (int)Math.Ceiling((double)vm.RecordCount / pgnum);
            if (vm.CurPageIndex > vm.PageTotal)
                vm.CurPageIndex = vm.PageTotal;
            if(vm.RecordCount>0)
                vm.CurPageStocks = StockService.GetPagedStocks(cat,(vm.CurPageIndex-1)*vm.PageSize,vm.PageSize,k);            
            return View(vm);
        }

        public ActionResult Buy(int? p, int? cat, string k, string area)
        {
            var vm = new ItemListViewModel();
            vm.FirstCatLevel = 0;
            if (cat.HasValue && cat.Value > 0)
            {
                vm.SubLevelCats = StockService.GetSubLevelStockCats(cat.Value);
                //if(vm.TopLevelCats==null||vm.TopLevelCats.Count<1)
                //    vm.TopLevelCats = StockService.GetTopLevelStockCats();
                //else
                //{
                var pcat = DBService.GetEntity<St_cat>(cat.Value);
                if (pcat != null && pcat.Id > 0)
                {
                    vm.FirstCatLevel = pcat.Level.HasValue ? pcat.Level.Value + 1 : 0;
                    if (!string.IsNullOrEmpty(pcat.Path))
                    {
                        vm.CatPath = StockService.GetStockCatPath(pcat.Path);
                    }
                    vm.CatID = cat.Value;
                    vm.CatName = pcat.Name;
                }
                //}
            }
            vm.TopLevelCats = StockService.GetTopLevelStockCats();

            vm.HotStocks = StockService.GetBuyHotStocks(10);

            var pgnum = 40;
            vm.PageSize = pgnum;
            vm.CurPageIndex = p.HasValue && p.Value > 1 ? p.Value : 1;
            vm.RecordCount = StockService.GetBuyTotalStockCount(cat, k);
            vm.PageTotal = (int)Math.Ceiling((double)vm.RecordCount / pgnum);
            if (vm.CurPageIndex > vm.PageTotal)
                vm.CurPageIndex = vm.PageTotal;
            if (vm.RecordCount > 0)
                vm.CurPageStocks = StockService.GetBuyPagedStocks(cat, (vm.CurPageIndex - 1) * vm.PageSize, vm.PageSize, k);
            return View(vm);
        }

        public ActionResult Brand(int? p, int? cat, string k, string area)
        {
            var vm = new ItemListViewModel();
            vm.FirstCatLevel = 0;
            if (cat.HasValue && cat.Value > 0)
            {
                vm.SubLevelCats = StockService.GetSubLevelStockCats(cat.Value);
                //if(vm.TopLevelCats==null||vm.TopLevelCats.Count<1)
                //    vm.TopLevelCats = StockService.GetTopLevelStockCats();
                //else
                //{
                var pcat = DBService.GetEntity<St_cat>(cat.Value);
                if (pcat != null && pcat.Id > 0)
                {
                    vm.FirstCatLevel = pcat.Level.HasValue ? pcat.Level.Value + 1 : 0;
                    if (!string.IsNullOrEmpty(pcat.Path))
                    {
                        vm.CatPath = StockService.GetStockCatPath(pcat.Path);
                    }
                    vm.CatID = cat.Value;
                    vm.CatName = pcat.Name;
                }
                //}
            }
            vm.TopLevelCats = StockService.GetTopLevelStockCats();

            vm.HotStocks = StockService.GetBrandHotStocks(10);

            var pgnum = 40;
            vm.PageSize = pgnum;
            vm.CurPageIndex = p.HasValue && p.Value > 1 ? p.Value : 1;
            vm.RecordCount = StockService.GetBrandTotalStockCount(cat, k);
            vm.PageTotal = (int)Math.Ceiling((double)vm.RecordCount / pgnum);
            if (vm.CurPageIndex > vm.PageTotal)
                vm.CurPageIndex = vm.PageTotal;
            if (vm.RecordCount > 0)
                vm.CurPageStocks = StockService.GetBrandPagedStocks(cat, (vm.CurPageIndex - 1) * vm.PageSize, vm.PageSize, k);
            return View(vm);
        }
               

        [Authorize]
        [TokenFilter]
        public ActionResult Publish()
        {
            ViewBag.PCats = StockService.GetStockCats();
            ViewBag.IsVip = false;//当前用户
            ViewBag.IsAdmin = User.IsInRole("1") || User.IsInRole("-1");
            var uid = GetCurUserId();
            if (User.Identity.IsAuthenticated && uid > 0)
            {
                var memb = MemberService.GetMemberFromUser(uid);
                ViewBag.Member = memb;
                if (memb != null && memb.Id > 0 && memb.Level != "" && memb.Level != "普通会员" && memb.VipOverDate.HasValue && memb.VipOverDate.Value > DateTime.Now)                
                    ViewBag.IsVip = true;
            }
            return View();
        }

        [Authorize]
        [TokenFilter]
        [HttpPost]
        [ActionName("Publish")]
        [ValidateAntiForgeryToken]
        [ValidateInput(false)]
        public ActionResult PublishPOST(ItemPublishViewModel model)
        {
            //if (model.MainPic == null || model.MainPic.ContentLength == 0)
            //    ModelState.AddModelError("MainPic", "主图不能为空！");
            if (ModelState.IsValid)
            {
                var item = new St_good();
                item.AddTime = DateTime.Now;
                bool isVip = false;
                int uid = GetCurUserId();
                if (uid > 0)
                {
                    item.AddUser = uid;
                    var member = MemberService.GetMemberFromUser(uid);
                    if (member != null && member.Id > 0)
                        item.MemberId = member.Id;
                    if (member != null && member.Id > 0 && member.Level != "" && member.Level != "普通会员" && member.VipOverDate.HasValue && member.VipOverDate.Value > DateTime.Now)
                        isVip = true;
                }
                bool isAdmin = User.IsInRole("1") || User.IsInRole("-1");
                if ((isVip || isAdmin) && model.GoodsType == 0)
                    item.GoodsType = 0;
                else
                    item.GoodsType = 1;
                item.Details = model.Details;
                if (model.MainPic != null && model.MainPic.ContentLength > 0)
                {
                    try
                    {
                        DateTime now = DateTime.Now;
                        string vpath = "/Site/Goods/" + now.ToString("yyyyMMdd") + "/";
                        string realpath = Server.MapPath(vpath);
                        if (!Directory.Exists(realpath))
                            Directory.CreateDirectory(realpath);
                        string fileext = Path.GetExtension(model.MainPic.FileName);
                        if (string.IsNullOrEmpty(fileext) || !fileext.StartsWith("."))
                            fileext = ".png";
                        string filename = Guid.NewGuid().ToString() + fileext.ToLower();
                        //if (System.IO.File.Exists(realpath + filename))
                        //{
                        //    filename = now.Ticks.ToString() + "_" + filename;
                        //}
                        model.MainPic.SaveAs(realpath + filename);
                        item.MainPic = vpath + filename;
                    }
                    catch
                    {

                    }
                }
                else
                    item.MainPic = "/Images/stock.jpg";
                item.CatId = model.CatId;
                if (model.CatId > 0)
                {
                    var ccat = DBService.GetEntity<St_cat>(model.CatId);
                    if (ccat != null && ccat.Id > 0)
                        item.CatPath = ccat.Path;
                }
                item.ModifyTime = DateTime.Now;
                item.Name = model.Name;
                item.Price = model.Price;
                item.PriceDetail = model.PriceDetail;
                if (!item.Price.HasValue && string.IsNullOrEmpty(item.PriceDetail))
                    item.PriceDetail = "详谈";
                item.Qty = model.Qty;
                item.QtyDetail = model.QtyDetail;
                item.Status = 0;
                item.ViewCount = 0;
                item.ContactViewCount = 0;
                item.GoodsArea = model.GoodsArea??"";
                item.BrandName = model.BrandName;//后台设置isbrand
                item.RealName = model.RealName;
                item.Email = model.Email;
                item.Mobile = model.Mobile;
                item.Tel = model.Tel;
                item.QQ = model.QQ;
                item.Wechat = model.Wechat;
                item.Addr = model.Addr;
                int gid = DBService.AddEntity<St_good>(item,true);
                if (gid > 0 && !string.IsNullOrEmpty(model.OPics))
                {
                    var aids = model.OPics.Split(',');
                    var picids = new List<int>();
                    int tempid=0;
                    foreach (var aid in aids)
                        if (!string.IsNullOrEmpty(aid) && int.TryParse(aid, out tempid))
                            picids.Add(tempid);
                    if (picids.Count > 0)
                    {
                        StockService.UpdateStockPics(gid, picids);
                    }
                }
                if(isAdmin)
                    return Redirect("/Admin/Stock");
                else
                    return RedirectToAction("Stocks", "Manage");
            }
            ViewBag.IsVip = false;//当前用户
            ViewBag.IsAdmin = User.IsInRole("1") || User.IsInRole("-1");
            var tuid = GetCurUserId();
            if (User.Identity.IsAuthenticated && tuid > 0)
            {
                var memb = MemberService.GetMemberFromUser(tuid);
                ViewBag.Member = memb;
                if (memb != null && memb.Id > 0 && memb.Level != "" && memb.Level != "普通会员" && memb.VipOverDate.HasValue && memb.VipOverDate.Value > DateTime.Now)
                    ViewBag.IsVip = true;
            }
            ViewBag.PCats = StockService.GetStockCats();
            return View(model);
        }

        [Authorize]
        [TokenFilter]
        [HttpPost]
        public JsonResult PicUpload()
        {
            var r = new List<ViewDataUploadFilesResult>();
            if (Request.Files.Count > 0)
            {
                try
                {
                    DateTime now = DateTime.Now;
                    string vpath = "/Site/Goods/" + now.ToString("yyyyMMdd") + "/";
                    string realpath = Server.MapPath(vpath);
                    if (!Directory.Exists(realpath))
                        Directory.CreateDirectory(realpath);
                    for (int i = 0; i < Request.Files.Count; i++)
                    {
                        var file = Request.Files[i];
                        string fileext = Path.GetExtension(file.FileName);
                        if (string.IsNullOrEmpty(fileext) || !fileext.StartsWith("."))
                            fileext = ".png";
                        string fname = Guid.NewGuid().ToString() + fileext.ToLower();
                        //if (System.IO.File.Exists(realpath + fname))
                        //{
                        //    fname = now.Ticks.ToString() + "_" + fname;
                        //}
                        file.SaveAs(realpath + fname);

                        var pic = new St_pic();
                        pic.AddTime = now;
                        pic.PicType = 5;
                        pic.PicUrl = vpath + fname;
                        int pid=DBService.AddEntity<St_pic>(pic,true);

                        r.Add(new ViewDataUploadFilesResult()
                        {
                            ppid=pid,
                            name = fname,
                            size = file.ContentLength,
                            type = file.ContentType,
                            url = vpath + fname
                        });
                    }
                }
                catch
                {

                }
            }

            return Json(r);
        }

        public ActionResult Show(int id)
        {
            var vm = new ItemShowViewModel();
            vm.FirstCatLevel = 0;
            if (id > 0)
            {
                vm.Stock = DBService.GetEntity<St_good>(id);
                vm.StockPics = StockService.GetStockPics(id);

                vm.TopLevelCats = StockService.GetTopLevelStockCats();
                if (vm.Stock.CatId.HasValue)
                {
                    vm.SubLevelCats = StockService.GetSubLevelStockCats(vm.Stock.CatId.Value);
                    var pcat = DBService.GetEntity<St_cat>(vm.Stock.CatId.Value);
                    if (pcat != null && pcat.Id > 0)
                    {
                        vm.CatID = pcat.Id;
                        vm.CatName = pcat.Name;
                        vm.FirstCatLevel = pcat.Level.HasValue ? pcat.Level.Value + 1 : 0;
                        if(!string.IsNullOrEmpty(pcat.Path))
                        {
                            vm.CatPath = StockService.GetStockCatPath(pcat.Path);
                        }
                    }
                }
            }

            vm.HotStocks = StockService.GetHotStocks(10);

            vm.IsVip = false;//当前用户
            var uid = GetCurUserId();
            if (User.Identity.IsAuthenticated && uid>0)
            {
                var memb= MemberService.GetMemberFromUser(uid);
                if (memb != null && memb.Id > 0 && memb.Level != "" && memb.Level != "普通会员" && memb.VipOverDate.HasValue && memb.VipOverDate.Value > DateTime.Now)
                    vm.IsVip = true;
            }
            if (vm.Stock.MemberId.HasValue)
                vm.Member = DBService.GetEntity<St_member>(vm.Stock.MemberId.Value);
            else if (vm.Stock.AddUser.HasValue)
                vm.Member = MemberService.GetMemberFromUser(vm.Stock.AddUser.Value);

            StockService.UpdateViewCount(id);

            return View(vm);
        }
    }

    public class ViewDataUploadFilesResult
    {
        public int ppid { get; set; }
        public string name { get; set; }
        public int size { get; set; }
        public string type { get; set; }
        public string url { get; set; }
        //public string delete_url { get; set; }
        //public string thumbnail_url { get; set; }
        //public string delete_type { get; set; }
    }
}