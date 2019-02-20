using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using StockNet.Model;

namespace StockNet.Areas.Admin.Models
{
    public class UserAddEditViewModel
    {
        public St_user User { get; set; }

        [Required]
        [Display(Name="用户名不能为空！")]
        public string username { get; set; }

        [Required]
        [Display(Name = "密码不能为空！")]
        public string userpass { get; set; }
        public string realname { get; set; }
        public string email { get; set; }
        public int usersex { get; set; }

        [Required]
        [Display(Name = "角色不能为空！")]
        public int userrole { get; set; }

        public IList<St_role> Roles { get; set; }
    }
}