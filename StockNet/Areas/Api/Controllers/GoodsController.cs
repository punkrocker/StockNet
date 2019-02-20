using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Newtonsoft.Json;
using ServiceStack.Common.Extensions;
using StockNet.Areas.Api.Models;
using StockNet.Model;
using StockNet.Models;
using StockNet.Service;

namespace StockNet.Areas.Api.Controllers
{
    public class GoodsController : Controller
    {
        public ActionResult GetCats()
        {
            var catList = StockService.GetStockCats();
            return Content(JsonConvert.SerializeObject(catList));
        }

        public ActionResult GetAllItems(int limit)
        {
            var Items = StockService.GetlatestStocks(limit);
            var result = new List<St_good>();
            foreach (var good in Items)
            {
                good.Details = WebHelper.StripHtml(good.Details);
                result.Add(good);
            }
            return Content(JsonConvert.SerializeObject(result));
        }

        public ActionResult GetHotItems(int limit)
        {
            var Items = StockService.GetHotStocks(limit);
            var result = new List<St_good>();
            foreach (var good in Items)
            {
                good.Details = WebHelper.StripHtml(good.Details);
                result.Add(good);
            }
            return Content(JsonConvert.SerializeObject(result));
        }

        public ActionResult GetItems(int? catid, int start, int limit, string search)
        {
            var Items = StockService.GetPagedStocks(catid, start, limit, search);
            var result = new List<St_good>();
            foreach (var good in Items)
            {
                good.Details = WebHelper.StripHtml(good.Details);
                result.Add(good);
            }
            return Content(JsonConvert.SerializeObject(result));
        }

        public ActionResult GetBuyItem(int? catid, int start, int limit, string search)
        {
            var Items = StockService.GetBuyPagedStocks(catid, start, limit, search);
            var result = new List<St_good>();
            foreach (var good in Items)
            {
                good.Details = WebHelper.StripHtml(good.Details);
                result.Add(good);
            }
            return Content(JsonConvert.SerializeObject(result));
        }

        public ActionResult GetBrandItems(int? catid, int start, int limit, string search)
        {
            var Items = StockService.GetBrandPagedStocks(catid, start, limit, search);
            var result = new List<St_good>();
            foreach (var good in Items)
            {
                good.Details = WebHelper.StripHtml(good.Details);
                result.Add(good);
            }
            return Content(JsonConvert.SerializeObject(result));
        }

        [HttpPost]
        public ActionResult PublishItem(PublishItem model)
        {
            WebResult result = new WebResult
            {
                Code = AppConst.MSG_ERR,
                Message = string.Empty
            };

            bool IsVip = false;//当前用户
            if (model.uid > 0)
            {
                var memb = MemberService.GetMemberFromUser(model.uid);
                if (memb != null && memb.Id > 0 && memb.Level != "" && memb.Level != "普通会员" && memb.VipOverDate.HasValue && memb.VipOverDate.Value > DateTime.Now)
                    IsVip = true;
            }
            //判断是否出售且用户为vip
            if (model.GoodsType==0 && !IsVip)
            {
                result.Message = "非VIP用户不能发布出售信息！";
                return Content(JsonConvert.SerializeObject(result));
            }

            if (model.MainPicName.Equals(string.Empty))
            {
                //result.Message = "主图不能为空！";
                //return Content(JsonConvert.SerializeObject(result));
                model.MainPicName = "/Images/stock.jpg";
            }

            //Save Item
            var item = new St_good();
            item.AddTime = DateTime.Now;
            int uid = model.uid;
            if (uid > 0)
            {
                item.AddUser = uid;
                var member = MemberService.GetMemberFromUser(uid);
                if (member != null && member.Id > 0)
                    item.MemberId = member.Id;
            }
            item.Details = model.Details;
            if (!model.MainPicName.Equals(string.Empty))
            {
                try
                {
                    //DateTime now = DateTime.Now;
                    //string vpath = "/Site/Goods/" + now.ToString("yyyyMMdd") + "/";
                    //string realpath = Server.MapPath(vpath);
                    //if (!Directory.Exists(realpath))
                    //Directory.CreateDirectory(realpath);
                    //string filename = model.MainPic.FileName;
                    //if (System.IO.File.Exists(realpath + filename))
                    //{
                    //filename = now.Ticks.ToString() + "_" + filename;
                    //}
                    //model.MainPic.SaveAs(realpath + filename);
                    item.MainPic = model.MainPicName;
                }
                catch
                {

                }
            }
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
            item.Qty = model.Qty;
            item.QtyDetail = model.QtyDetail;
            item.Status = 0;
            item.ViewCount = 0;
            item.ContactViewCount = 0;
            item.GoodsType = model.GoodsType;
            item.RealName = model.RealName;
            item.QQ = model.QQ;
            item.Mobile = model.Mobile;
            item.Tel = model.Tel;
            item.Wechat = model.Wechat;
            item.Addr = model.Addr;
            int gid = DBService.AddEntity<St_good>(item, true);
            if (gid > 0 && !string.IsNullOrEmpty(model.OPics))
            {
                var aids = model.OPics.Split(',');
                var picids = new List<int>();
                int tempid = 0;
                foreach (var aid in aids)
                {
                    var pic = new St_pic();
                    pic.AddTime = DateTime.Now;
                    pic.PicType = 5;
                    pic.PicUrl = aid;
                    int pid = DBService.AddEntity<St_pic>(pic, true);
                    //if (!string.IsNullOrEmpty(pid) && int.TryParse(pid, out tempid))
                    if (pid>0)
                        picids.Add(pid);
                }
                if (picids.Count > 0)
                {
                    StockService.UpdateStockPics(gid, picids);
                }
            }

            //return RedirectToAction("Stocks", "Manage");
            //ViewBag.PCats = StockService.GetStockCats();
            //return View(model);
            result.Code = AppConst.MSG_SUCCESS;
            return Content(JsonConvert.SerializeObject(result));
        }

        public ActionResult GetDetail(int id,int usrid)
        {
            var vm = new ItemShowViewModel();
            vm.FirstCatLevel = 0;
            if (id > 0)
            {
                vm.Stock = DBService.GetEntity<St_good>(id);
                vm.StockPics = StockService.GetStockPics(id);

                if (vm.Stock.CatId.HasValue)
                {
                    vm.TopLevelCats = StockService.GetSubLevelStockCats(vm.Stock.CatId.Value);
                    var pcat = DBService.GetEntity<St_cat>(vm.Stock.CatId.Value);
                    if (pcat != null && pcat.Id > 0)
                    {
                        vm.CatID = pcat.Id;
                        vm.CatName = pcat.Name;
                        vm.FirstCatLevel = pcat.Level.HasValue ? pcat.Level.Value + 1 : 0;
                        if (!string.IsNullOrEmpty(pcat.Path))
                        {
                            vm.CatPath = StockService.GetStockCatPath(pcat.Path);
                        }
                    }
                }
            }

            vm.HotStocks = StockService.GetHotStocks(10);

            vm.IsVip = false;//当前用户
            var uid = usrid;
            if ( uid > 0)
            {
                var memb = MemberService.GetMemberFromUser(uid);
                if (memb != null && memb.Id > 0 && memb.Level != "" && memb.Level != "普通会员" && memb.VipOverDate.HasValue && memb.VipOverDate.Value > DateTime.Now)
                    vm.IsVip = true;
            }
            if (vm.Stock.MemberId.HasValue)
                vm.Member = DBService.GetEntity<St_member>(vm.Stock.MemberId.Value);

            StockService.UpdateViewCount(id);
            vm.Stock.Details = WebHelper.StripHtml(vm.Stock.Details);

            ItemDetail item = new ItemDetail
            {
                IsVip = vm.IsVip,
                Stock = vm.Stock,
                StockPics = vm.StockPics
            };

            if (!vm.IsVip)
            {
                vm.Stock.RealName = string.Empty;
                vm.Stock.Tel = string.Empty;
                vm.Stock.Mobile = string.Empty;
                vm.Stock.Wechat = string.Empty;
                vm.Stock.Addr = string.Empty;
            }

            return Content(JsonConvert.SerializeObject(item));
        }

        public ActionResult AreaList()
        {
            string[] areas =
            {
                "北京", "安徽", "重庆", "福建", "广东", "甘肃", "广西", "贵州", "湖北", "湖南", "河北", "河南", "海南", "黑龙江", "江苏",
                "吉林", "江西", "辽宁", "内蒙古", "宁夏", "青海", "山东", "上海", "山西", "陕西", "四川", "天津", "新疆", "西藏", "云南", "浙江"
            };
            return Content(JsonConvert.SerializeObject(areas));
        }

    }
}