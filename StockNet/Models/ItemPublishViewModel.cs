using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace StockNet.Models
{
    public class ItemPublishViewModel
    {
        [Required]
        [Display(Name = "标题")]
        public string Name { get; set; }        

        [Required]
        [Display(Name = "分类")]
        public int CatId { get; set; }

        [Display(Name = "价格")]
        public double? Price { get; set; }

        [Display(Name = "价格描述")]
        public string PriceDetail { get; set; }

        [Required]
        [Display(Name = "数量")]
        public int? Qty { get; set; }

        [Display(Name = "数量描述")]
        public string QtyDetail { get; set; }

        [Display(Name = "主图")]
        public HttpPostedFileBase MainPic { get; set; }

        public string OPics { get; set; }

        [Required]
        [Display(Name = "详细内容")]
        public string Details { get; set; }

        [Display(Name = "发布类型")]
        public int GoodsType { get; set; }
                
        [Display(Name = "区域")]
        public string GoodsArea { get; set; }

        [Display(Name = "品牌")]
        public string BrandName { get; set; }

        [Required]
        [Display(Name = "姓名")]
        public string RealName { get; set; }

        [Display(Name = "电子邮件")]
        public string Email { get; set; }

        [Required]
        [Display(Name = "手机")]
        public string Mobile { get; set; }

        [Display(Name = "电话")]
        public string Tel { get; set; }

        [Display(Name = "QQ")]
        public string QQ { get; set; }

        [Display(Name = "微信")]
        public string Wechat { get; set; }

        [Display(Name = "地址")]
        public string Addr { get; set; }

        [Display(Name = "其它")]
        public string Other { get; set; } 
    }
}