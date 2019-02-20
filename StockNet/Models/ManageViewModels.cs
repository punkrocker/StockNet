using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Microsoft.Owin.Security;
using StockNet.Model;

namespace StockNet.Models
{
    public class MemberIndexViewModel
    {
        public string Level { get; set; }
    }

    public class SetPasswordViewModel
    {
        [Required]
        [StringLength(100, ErrorMessage = "{0} 必须至少包含 {2} 个字符。", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "新密码")]
        public string NewPassword { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "确认新密码")]
        [Compare("NewPassword", ErrorMessage = "新密码和确认密码不匹配。")]
        public string ConfirmPassword { get; set; }
    }

    public class ChangePasswordViewModel
    {
        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "当前密码")]
        public string OldPassword { get; set; }

        [Required]
        [StringLength(100, ErrorMessage = "{0} 必须至少包含 {2} 个字符。", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "新密码")]
        public string NewPassword { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "确认新密码")]
        [Compare("NewPassword", ErrorMessage = "新密码和确认密码不匹配。")]
        public string ConfirmPassword { get; set; }
    }

    public class ChangeInfoViewModel
    {
        [Required]
        [Display(Name = "姓名")]
        public string RealName { get; set; }

        [Required]
        [EmailAddress]
        [Display(Name = "电子邮件")]
        public string Email { get; set; }

        [Required]
        [Display(Name = "手机")]
        public string Mobile { get; set; }

        [Display(Name = "电话")]
        public string Tel { get; set; }

        [Required]
        [Display(Name = "QQ")]
        public string QQ { get; set; }

        [Display(Name = "微信")]
        public string Wechat { get; set; }

        [Display(Name = "地址")]
        public string Addr { get; set; }

        [Display(Name = "其它")]
        public string Other { get; set; } 
    }

    public class MemberStockViewModel
    {
        public IList<St_good> CurPageStocks { get; set; }
        public int CurPageIndex { get; set; }
        public int PageSize { get; set; }
        public int PageTotal { get; set; }
        public int RecordCount { get; set; }
    }
}