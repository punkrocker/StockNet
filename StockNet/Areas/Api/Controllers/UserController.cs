using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Web;
using System.Web.Mvc;
using Microsoft.Owin.Security;
using Newtonsoft.Json;
using StockNet.Areas.Api.Models;
using StockNet.Model;
using StockNet.Models;
using StockNet.Service;

namespace StockNet.Areas.Api.Controllers
{
    public class UserController : Controller
    {
        [HttpPost]
        public ActionResult Register(RegisterViewModel model)
        {
            RegisterResult result = new RegisterResult
            {
                Code = AppConst.MSG_ERR,
                Message = "",
                Data = null
            };
            try
            {
                var user = new St_user();
                user.AddTime = DateTime.Now;
                user.LoginName = model.UserName;
                user.LoginPass = DBService.SHA1Hash(model.Password);
                user.Name = model.UserName;
                user.Email = "";
                user.RoleId = 0;
                user.Status = 0;
                int uid = DBService.AddEntity<St_user>(user, true);

                if (uid > 0)
                {
                    var member = new St_member();
                    member.Addr = "";
                    member.AddTime = DateTime.Now;
                    member.Email = "";
                    member.HadVerify = false;
                    member.Level = "普通会员";
                    member.Mobile = model.Mobile;
                    member.QQ = "";
                    member.RealName = model.UserName;
                    member.Remark = "";
                    member.Tel = "";
                    member.UserId = uid;
                    member.Wechat = "";
                    int memid = DBService.AddEntity<St_member>(member, true);
                    if (memid > 0)
                    {
                        var claims = new List<Claim>();
                        claims.Add(new Claim(ClaimTypes.Name, model.UserName));
                        claims.Add(new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()));
                        claims.Add(
                            new Claim(
                                "http://schemas.microsoft.com/accesscontrolservice/2010/07/claims/identityprovider",
                                "ASP.NET Identity"));
                        claims.Add(new Claim(ClaimTypes.Role, "0"));
                        claims.Add(new Claim("Sex", user.Sex.HasValue && user.Sex.Value ? "1" : "0"));
                        claims.Add(new Claim("DisplayName", user.Name));

                        var identity = new ClaimsIdentity(claims, "MyClaimsLogin");
                        HttpContext.GetOwinContext().Authentication.SignIn(new AuthenticationProperties()
                        {
                            IsPersistent = false
                        }, identity);
                        result.Code = AppConst.MSG_SUCCESS;
                        result.Data = model;
                    }
                    else
                    {
                        ModelState.AddModelError("", "无法注册用户！");
                        result.Message = "无法注册用户！";
                    }
                }
                else
                {
                    ModelState.AddModelError("", "无法添加用户！");
                    result.Message = "无法添加用户！";
                }
            }
            catch (Exception ex)
            {
                result.Message = ex.Message;
            }

            return Content(JsonConvert.SerializeObject(result));
        }

        [HttpPost]
        public ActionResult Login(LoginViewModel model)
        {
            RegisterResult result = new RegisterResult
            {
                Code = AppConst.MSG_ERR,
                Message = "",
                Data = null
            };
            try
            {
                St_user identity = UserService.CheckUser(model.UserName, model.Password);

                if (identity != null)
                {
                    //AuthenticationManager.SignOut();
                    //HttpContext.GetOwinContext().Authentication.SignIn(new AuthenticationProperties()
                    //{
                    //    IsPersistent = model.RememberMe == "on"
                    //}, identity);
                    result.Code = AppConst.MSG_SUCCESS;
                    result.Data = identity;
                }
                else
                {
                    result.Message = "无效的登录尝试";
                }
            }
            catch (Exception ex)
            {
                result.Message = ex.Message;
            }
            return Content(JsonConvert.SerializeObject(result));
        }

        public ActionResult GetUser(int uid)
        {
            WebResult result = new WebResult
            {
                Code = AppConst.MSG_ERR,
                Message = "",
                Data = null
            };
            try
            {
                var member = MemberService.GetMemberFromUser(uid);
                result.Code = AppConst.MSG_SUCCESS;
                result.Data = member;
            }
            catch (Exception exception)
            {
                result.Message = exception.Message;
            }

            return Content(JsonConvert.SerializeObject(result));
        }

        [HttpPost]
        public ActionResult ChangeTel(int uid, string newInfo)
        {
            WebResult result = new WebResult
            {
                Code = AppConst.MSG_ERR,
                Message = "",
                Data = null
            };
            try
            {
                var member = MemberService.GetMemberFromUser(uid);
                if (member != null && member.Id > 0)
                {
                    member.Mobile = newInfo;
                    DBService.UpdateEntity<St_member>(member);
                }
                result.Code = AppConst.MSG_SUCCESS;
            }
            catch (Exception exception)
            {
                result.Message = exception.Message;
            }

            return Content(JsonConvert.SerializeObject(result));
        }

        [HttpPost]
        public ActionResult ChangeEmail(int uid, string newInfo)
        {
            WebResult result = new WebResult
            {
                Code = AppConst.MSG_ERR,
                Message = "",
                Data = null
            };
            try
            {
                var member = MemberService.GetMemberFromUser(uid);
                if (member != null && member.Id > 0)
                {
                    member.Email = newInfo;
                    DBService.UpdateEntity<St_member>(member);
                }
                result.Code = AppConst.MSG_SUCCESS;
            }
            catch (Exception exception)
            {
                result.Message = exception.Message;
            }

            return Content(JsonConvert.SerializeObject(result));
        }

        public ActionResult GetMyStock(int mid, int start, int limit, string search)
        {
            var myStock = StockService.GetMemberPagedStocks(mid, start, limit, search);
            return Content(JsonConvert.SerializeObject(myStock));
        }
    }
}