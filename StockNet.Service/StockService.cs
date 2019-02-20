using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ServiceStack.OrmLite;
using StockNet.Model;

namespace StockNet.Service
{
    public class StockService
    {
        /// <summary>
        /// 后台获取库存个数
        /// </summary>
        /// <param name="memberid"></param>
        /// <param name="search"></param>
        /// <returns></returns>
        public static int GetStockCount(int? memberid, string search)
        {
            using (var db = DBService.OpenDb())
            {
                if (!string.IsNullOrEmpty(search))
                {
                    if (memberid > 0)
                        return (int)db.Count<St_good>(fs => fs.MemberId == memberid && fs.Name.Contains(search));
                    else
                        return (int)db.Count<St_good>(fs => fs.Name.Contains(search));
                }
                else if (memberid > 0)
                    return (int)db.Count<St_good>(fs => fs.MemberId == memberid);
                else
                    return (int)db.Count<St_good>();
            }
        }

        /// <summary>
        /// 后台获取库存
        /// </summary>
        /// <param name="memberid"></param>
        /// <param name="start"></param>
        /// <param name="limit"></param>
        /// <param name="search"></param>
        /// <returns></returns>
        public static IList<St_good> GetStocks(int? memberid, int start, int limit, string search)
        {
            using (var db = DBService.OpenDb())
            {
                if (!string.IsNullOrEmpty(search))
                {
                    if (memberid > 0)
                        return db.Select<St_good>(fs => fs.Limit(start, limit).Where(ct => ct.MemberId == memberid && ct.Name.Contains(search)).OrderByDescending(ct => ct.Id));
                    else
                        return db.Select<St_good>(fs => fs.Limit(start, limit).Where(ct => ct.Name.Contains(search)).OrderByDescending(ct => ct.Id));
                }
                else if (memberid > 0)
                    return db.Select<St_good>(fs => fs.Limit(start, limit).Where(ct => ct.MemberId == memberid).OrderByDescending(ct => ct.Id));
                else
                    return db.Select<St_good>(fs => fs.Limit(start, limit).OrderByDescending(ct => ct.Id));
            }
        }

        /// <summary>
        /// 前台获取热门库存出售
        /// </summary>
        /// <param name="limit"></param>
        /// <returns></returns>
        public static IList<St_good> GetHotStocks(int limit)
        {
            using (var db = DBService.OpenDb())
            {
                return db.Select<St_good>(cat => cat.Limit(limit).Where(ct=>ct.Status>0&&ct.GoodsType!=1).OrderByDescending(ct => ct.ViewCount).OrderByDescending(ct => ct.Id));
            }
        }

        /// <summary>
        /// 前台获取热门库存求购
        /// </summary>
        /// <param name="limit"></param>
        /// <returns></returns>
        public static IList<St_good> GetBuyHotStocks(int limit)
        {
            using (var db = DBService.OpenDb())
            {
                return db.Select<St_good>(cat => cat.Limit(limit).Where(ct => ct.Status > 0 &&ct.GoodsType==1).OrderByDescending(ct => ct.ViewCount).OrderByDescending(ct => ct.Id));
            }
        }

        /// <summary>
        /// 前台获取热门品牌库存
        /// </summary>
        /// <param name="limit"></param>
        /// <returns></returns>
        public static IList<St_good> GetBrandHotStocks(int limit)
        {
            using (var db = DBService.OpenDb())
            {
                return db.Select<St_good>(cat => cat.Limit(limit).Where(ct =>ct.IsBrandGoods==true && ct.Status > 0).OrderByDescending(ct => ct.ViewCount).OrderByDescending(ct => ct.Id));
            }
        }

        /// <summary>
        /// 首页获取最新库存
        /// </summary>
        /// <param name="limit"></param>
        /// <returns></returns>
        public static IList<St_good> GetlatestStocks(int limit)
        {
            using (var db = DBService.OpenDb())
            {
                return db.Select<St_good>(cat => cat.Limit(limit).Where(ct => ct.Status > 0).OrderByDescending(ct => ct.Id));
            }
        }

        /// <summary>
        /// 首页获取推荐库存
        /// </summary>
        /// <param name="limit"></param>
        /// <returns></returns>
        public static IList<St_good> GetRecommendStocks(int limit)
        {
            using (var db = DBService.OpenDb())
            {
                return db.Select<St_good>(cat => cat.Limit(limit).Where(ct => ct.Status > 0 && ct.Recommended == true).OrderByDescending(ct => ct.Id));
            }
        }

        /// <summary>
        /// 前台获取库存出售个数
        /// </summary>
        /// <param name="catid"></param>
        /// <param name="search"></param>
        /// <returns></returns>
        public static int GetTotalStockCount(int? catid, string search)
        {
            using (var db = DBService.OpenDb())
            {
                var catp = ","+catid+",";
                if (catid > 0)
                {
                    var ccat = db.SingleOrDefault<St_cat>("Id={0}", catid);
                    if (ccat != null && ccat.Id > 0)
                        catp = ccat.Path;
                }
                var query = db.CreateExpression<St_good>().Where(s => s.Status > 0 && s.GoodsType != 1);

                if (!string.IsNullOrEmpty(search))
                {
                    query = query.Where(s => s.Name.Contains(search));
                }
                if (catid > 0)
                    query = query.Where(s => s.CatPath.StartsWith(catp));

                return (int)db.Count<St_good>(query);
            }
        }

        /// <summary>
        /// 前台获取库存出售分页数据
        /// </summary>
        /// <param name="catid"></param>
        /// <param name="start"></param>
        /// <param name="limit"></param>
        /// <param name="search"></param>
        /// <returns></returns>
        public static IList<St_good> GetPagedStocks(int? catid, int start, int limit, string search)
        {
            using (var db = DBService.OpenDb())
            {
                var catp = "," + catid + ",";
                if (catid > 0)
                {
                    var ccat = db.SingleOrDefault<St_cat>("Id={0}", catid);
                    if (ccat != null && ccat.Id > 0)
                        catp = ccat.Path;
                }

                var query = db.CreateExpression<St_good>().Where(s => s.Status > 0 && s.GoodsType != 1);

                if (!string.IsNullOrEmpty(search))
                {
                    query=query.Where(s => s.Name.Contains(search));
                }
                if (catid > 0)
                    query = query.Where(s => s.CatPath.StartsWith(catp));

                query = query.OrderByDescending(s => s.Id).Limit(start, limit);

                return db.Select<St_good>(query);

                //if (!string.IsNullOrEmpty(search))
                //{
                //    if (catid > 0)
                //        return db.Select<St_good>(fs => fs.Limit(start, limit).Where(ct => ct.CatPath.StartsWith(catp) && ct.Status > 0 && ct.Name.Contains(search)).OrderByDescending(ct => ct.Id));
                //    else
                //        return db.Select<St_good>(fs => fs.Limit(start, limit).Where(ct => ct.Status > 0 && ct.Name.Contains(search)).OrderByDescending(ct => ct.Id));
                //}
                //else if (catid > 0)
                //    return db.Select<St_good>(fs => fs.Limit(start, limit).Where(ct => ct.CatPath.StartsWith(catp) && ct.Status > 0).OrderByDescending(ct => ct.Id));
                //else
                //    return db.Select<St_good>(fs => fs.Limit(start, limit).Where(ct => ct.Status > 0).OrderByDescending(ct => ct.Id));
            }
        }

        /// <summary>
        /// 前台获取库存求购个数
        /// </summary>
        /// <param name="catid"></param>
        /// <param name="search"></param>
        /// <returns></returns>
        public static int GetBuyTotalStockCount(int? catid, string search)
        {
            using (var db = DBService.OpenDb())
            {
                var catp = "," + catid + ",";
                if (catid > 0)
                {
                    var ccat = db.SingleOrDefault<St_cat>("Id={0}", catid);
                    if (ccat != null && ccat.Id > 0)
                        catp = ccat.Path;
                }
                var query = db.CreateExpression<St_good>().Where(s => s.Status > 0 && s.GoodsType==1);

                if (!string.IsNullOrEmpty(search))
                {
                    query = query.Where(s => s.Name.Contains(search));
                }
                if (catid > 0)
                    query = query.Where(s => s.CatPath.StartsWith(catp));

                return (int)db.Count<St_good>(query);
            }
        }

        /// <summary>
        /// 前台获取库存求购分页数据
        /// </summary>
        /// <param name="catid"></param>
        /// <param name="start"></param>
        /// <param name="limit"></param>
        /// <param name="search"></param>
        /// <returns></returns>
        public static IList<St_good> GetBuyPagedStocks(int? catid, int start, int limit, string search)
        {
            using (var db = DBService.OpenDb())
            {
                var catp = "," + catid + ",";
                if (catid > 0)
                {
                    var ccat = db.SingleOrDefault<St_cat>("Id={0}", catid);
                    if (ccat != null && ccat.Id > 0)
                        catp = ccat.Path;
                }

                var query = db.CreateExpression<St_good>().Where(s => s.Status > 0 && s.GoodsType == 1);

                if (!string.IsNullOrEmpty(search))
                {
                    query = query.Where(s => s.Name.Contains(search));
                }
                if (catid > 0)
                    query = query.Where(s => s.CatPath.StartsWith(catp));

                query = query.OrderByDescending(s => s.Id).Limit(start, limit);

                return db.Select<St_good>(query);
            }
        }

        /// <summary>
        /// 前台获取品牌库存个数
        /// </summary>
        /// <param name="catid"></param>
        /// <param name="search"></param>
        /// <returns></returns>
        public static int GetBrandTotalStockCount(int? catid, string search)
        {
            using (var db = DBService.OpenDb())
            {
                var catp = "," + catid + ",";
                if (catid > 0)
                {
                    var ccat = db.SingleOrDefault<St_cat>("Id={0}", catid);
                    if (ccat != null && ccat.Id > 0)
                        catp = ccat.Path;
                }
                if (!string.IsNullOrEmpty(search))
                {
                    if (catid > 0)
                        return (int)db.Count<St_good>(fs =>fs.IsBrandGoods==true && fs.CatPath.StartsWith(catp) && fs.Status > 0 && fs.Name.Contains(search));
                    else
                        return (int)db.Count<St_good>(fs => fs.IsBrandGoods == true && fs.Status > 0 && fs.Name.Contains(search));
                }
                else if (catid > 0)
                    return (int)db.Count<St_good>(fs => fs.IsBrandGoods == true && fs.CatPath.StartsWith(catp) && fs.Status > 0);
                else
                    return (int)db.Count<St_good>(fs => fs.IsBrandGoods == true && fs.Status > 0);
            }
        }

        /// <summary>
        /// 前台获取品牌库存分页数据
        /// </summary>
        /// <param name="catid"></param>
        /// <param name="start"></param>
        /// <param name="limit"></param>
        /// <param name="search"></param>
        /// <returns></returns>
        public static IList<St_good> GetBrandPagedStocks(int? catid, int start, int limit, string search)
        {
            using (var db = DBService.OpenDb())
            {
                var catp = "," + catid + ",";
                if (catid > 0)
                {
                    var ccat = db.SingleOrDefault<St_cat>("Id={0}", catid);
                    if (ccat != null && ccat.Id > 0)
                        catp = ccat.Path;
                }

                if (!string.IsNullOrEmpty(search))
                {
                    if (catid > 0)
                        return db.Select<St_good>(fs => fs.Limit(start, limit).Where(ct => ct.IsBrandGoods == true && ct.CatPath.StartsWith(catp) && ct.Status > 0 && ct.Name.Contains(search)).OrderByDescending(ct => ct.Id));
                    else
                        return db.Select<St_good>(fs => fs.Limit(start, limit).Where(ct => ct.IsBrandGoods == true && ct.Status > 0 && ct.Name.Contains(search)).OrderByDescending(ct => ct.Id));
                }
                else if (catid > 0)
                    return db.Select<St_good>(fs => fs.Limit(start, limit).Where(ct => ct.IsBrandGoods == true && ct.CatPath.StartsWith(catp) && ct.Status > 0).OrderByDescending(ct => ct.Id));
                else
                    return db.Select<St_good>(fs => fs.Limit(start, limit).Where(ct => ct.IsBrandGoods == true && ct.Status > 0).OrderByDescending(ct => ct.Id));
            }
        }

        /// <summary>
        /// 获取库存的其他图片
        /// </summary>
        /// <param name="stockid"></param>
        /// <returns></returns>
        public static IList<St_pic> GetStockPics(int stockid)
        {
            using (var db = DBService.OpenDb())
            {
                return db.Select<St_pic>(pic => pic.PicType == 5 && pic.RelateId==stockid);
            }
        }

        /// <summary>
        /// 后台获取所有库存分类
        /// </summary>
        /// <returns></returns>
        public static IList<St_cat> GetStockCats()
        {
            using (var db = DBService.OpenDb())
            {
                return db.Select<St_cat>(cat=>cat.OrderBy(ct=>ct.Path));
            }
        }

        /// <summary>
        /// 获取前两层分类
        /// </summary>
        /// <returns></returns>
        public static IList<St_cat> GetTopLevelStockCats()
        {
            using (var db = DBService.OpenDb())
            {
                return db.Select<St_cat>(cat => cat.Where(ct=>ct.Level<2).OrderBy(ct => ct.Id));
            }
        }

        /// <summary>
        /// 获取某分类的两层子分类
        /// </summary>
        /// <param name="catid"></param>
        /// <returns></returns>
        public static IList<St_cat> GetSubLevelStockCats(int catid)
        {
            using (var db = DBService.OpenDb())
            {
                var pcat = db.SingleOrDefault<St_cat>("Id={0}", catid);
                if (pcat != null)
                    return db.Select<St_cat>(cat => cat.Where(ct => ct.Id!=catid && ct.Path.StartsWith(pcat.Path) && ct.Level < pcat.Level + 3).OrderBy(ct => ct.Id));
                else
                    return null;
            }
        }

        /// <summary>
        /// 获取最新库存的所有分类
        /// </summary>
        /// <param name="latestIds"></param>
        /// <returns></returns>
        public static IList<St_cat> GetlatestStocksCats(IList<int> latestIds)
        {
            using (var db = DBService.OpenDb())
            {
                return db.Select<St_cat>(cat => Sql.In(cat.Id,latestIds));
            }
        }

        public static int GetMemberTotalStockCount(int memberid, string search)
        {
            using (var db = DBService.OpenDb())
            {
                if (!string.IsNullOrEmpty(search))
                {
                    if (memberid > 0)
                        return (int)db.Count<St_good>(fs => fs.MemberId==memberid && fs.Name.Contains(search));
                    else
                        return 0;
                }
                else if (memberid > 0)
                    return (int)db.Count<St_good>(fs => fs.MemberId == memberid);
                else
                    return 0;
            }
        }

        public static IList<St_good> GetMemberPagedStocks(int memberid, int start, int limit, string search)
        {
            using (var db = DBService.OpenDb())
            {
                if (!string.IsNullOrEmpty(search))
                {
                    if (memberid > 0)
                        return db.Select<St_good>(fs => fs.Limit(start, limit).Where(ct => ct.MemberId==memberid && ct.Name.Contains(search)).OrderByDescending(ct => ct.Id));
                    else
                        return null;
                }
                else if (memberid > 0)
                    return db.Select<St_good>(fs => fs.Limit(start, limit).Where(ct => ct.MemberId == memberid).OrderByDescending(ct => ct.Id));
                else
                    return null;
            }
        }

        public static IList<KeyValuePair<int, string>> GetStockCatPath(string path)
        {
            using (var db = DBService.OpenDb())
            {
                var ids = new List<int>();
                var pa = path.Split(',');
                int pi=0;
                foreach (var pai in pa)
                    if (!string.IsNullOrEmpty(pai) && int.TryParse(pai, out pi))
                        ids.Add(pi);
                var cats = db.Select<St_cat>(cat => Sql.In(cat.Id, ids));
                var res = new List<KeyValuePair<int, string>>();
                foreach (var cat in cats)
                {
                    res.Add(new KeyValuePair<int, string>(cat.Id, cat.Name));
                }
                return res;
            }
        }

        public static void UpdateViewCount(int stockid)
        {
            if (stockid > 0)
            {
                using (var db = DBService.OpenDb())
                {
                    db.Update<St_good>("ViewCount=ViewCount+1", "Id="+stockid);
                }
            }
        }

        public static void UpdateCatPath(int id,string newpath)
        {
            if (id > 0 && !string.IsNullOrEmpty(newpath))
            {
                using (var db = DBService.OpenDb())
                {
                    int lv = 0;
                    if (newpath.IndexOf(',') > -1)
                    {
                        var pa = newpath.Split(',');
                        if (pa.Length > 3)
                            lv = pa.Length - 3;
                    }
                    db.Update<St_cat>(new { Path = newpath, Level = lv }, cc => cc.Id == id);
                }
            }
        }

        public static void UpdateSubCatPath(int parentid,string parentoldpath, string parentnewpath)
        {
            if (parentid>0 && !string.IsNullOrEmpty(parentoldpath) && !string.IsNullOrEmpty(parentnewpath))
            {
                using (var db = DBService.OpenDb())
                {
                    var subs = db.Select<St_cat>(cc => cc.Path.StartsWith(parentoldpath));
                    if (subs != null && subs.Count > 0)
                    {
                        foreach (var sub in subs)
                        {
                            if (sub.Id != parentid)
                            {
                                int lv = 0;
                                string newpath = parentnewpath+sub.Id.ToString()+",";
                                if (sub.Path.IndexOf(',') > -1)
                                {
                                    newpath = parentnewpath + sub.Path.Substring(parentoldpath.Length);                                    
                                    var pa = newpath.Split(',');
                                    if (pa.Length > 3)
                                        lv = pa.Length - 3;
                                }
                                db.Update<St_cat>(new { Path = newpath, Level = lv }, cc => cc.Id == sub.Id);
                            }
                        }
                    }                    
                }
            }
        }

        public static void UpdateStockCatPath(int catid, string catpath)
        {
            if (catid > 0 && !string.IsNullOrEmpty(catpath))
            {
                using (var db = DBService.OpenDb())
                {
                    db.Update<St_good>(new { CatPath = catpath }, cc => cc.CatId == catid);
                }
            }
        }

        public static void UpdateStockPics(int stockid, IList<int> picids)
        {
            if (stockid > 0 && picids!=null && picids.Count > 0)
            {
                using (var db = DBService.OpenDb())
                {
                    db.Update<St_pic>(new { RelateId = stockid }, cc => cc.PicType==5 && cc.AddTime>DateTime.Now.AddHours(-6) && Sql.In(cc.Id,picids));
                }
            }
        }

        public static void DeleteCat(int id)
        {
            if (id > 0)
            {
                using (var db = DBService.OpenDb())
                {
                    var acat=db.Single<St_cat>("Id={0}",id);
                    if(!string.IsNullOrEmpty(acat.Path))
                    {
                        db.Delete<St_cat>(cat => cat.Path.StartsWith(acat.Path));
                    }
                    else
                    {
                        db.Delete<St_cat>(cat => cat.Id == id);
                    }
                }
            }
        }
    }
}
