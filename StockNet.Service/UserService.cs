using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Security.Claims;
using ServiceStack.OrmLite;
using StockNet.Model;

namespace StockNet.Service
{
    public class UserService
    {
        public static ClaimsIdentity CheckIdentity(string userName, string pass)
        {
            return CheckIdentity(userName, pass, false);
        }
        public static ClaimsIdentity CheckIdentity(string userName, string pass,bool fromadmin)
        {
            if (!string.IsNullOrEmpty(userName) && !string.IsNullOrEmpty(pass))
            {
                using (var db = DBService.OpenDb())
                {
                    var user = db.SingleOrDefault<St_user>("LoginName={0} AND LoginPass={1} AND Status<5", userName, DBService.SHA1Hash(pass));
                    if (user != null && user.Id > 0)
                    {
                        var claims = new List<Claim>();
                        claims.Add(new Claim(ClaimTypes.Name, userName));
                        claims.Add(new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()));
                        claims.Add(new Claim("http://schemas.microsoft.com/accesscontrolservice/2010/07/claims/identityprovider", "ASP.NET Identity"));
                        int rid = 0;
                        if (fromadmin && user.RoleId.HasValue && user.RoleId.Value != 0)
                        {
                            rid=user.RoleId.Value == -1 && user.LoginName.Equals("admin", StringComparison.InvariantCultureIgnoreCase) ? -1 : 1;
                        }
                        claims.Add(new Claim(ClaimTypes.Role, rid.ToString()));
                        claims.Add(new Claim("Sex", user.Sex.HasValue && user.Sex.Value ? "1" : "0"));
                        claims.Add(new Claim("DisplayName", user.Name??user.LoginName));
                        
                        //管理员5分钟之内禁止再次登录，会员2分钟
                        var minutes = fromadmin ? 5 : 2;
                        if (!user.LastTime.HasValue || string.IsNullOrEmpty(user.LoginToken) || (user.LastTime.HasValue && user.LastTime.Value < DateTime.Now.AddMinutes(-minutes)))
                        {
                            var token = Guid.NewGuid().ToString();
                            db.Update<St_user>(new { LoginToken = token, LastTime=DateTime.Now }, us => us.Id == user.Id);
                            claims.Add(new Claim("Token", token));
                        }
                        else
                            return null;                        

                        var identity = new ClaimsIdentity(claims, "MyClaimsLogin");
                        return identity;
                    }
                }
            }

            return null;
        }

        public static string GetRoleName(string roleId)
        {
            int rid;
            if (string.IsNullOrEmpty(roleId) || !int.TryParse(roleId, out rid))
            {
                return "普通用户";
            }
            else if (rid > 0)
            {
                using (var db = DBService.OpenDb())
                {
                    var role = db.Single<St_role>("Id={0}", rid);
                    if (role != null && role.Id > 0)
                        return role.Name;
                    else
                        return "管理员";
                }
            }
            else if (rid == -1)
            {
                return "超级管理员";
            }
            else
                return "普通用户";
        }

        public static string GetRoleNameByUserID(string userId)
        {
            int uid;
            string rid = "";
            if(!string.IsNullOrEmpty(userId)&&int.TryParse(userId,out uid))
            {
                if(uid>0)
                {
                    using (var db = DBService.OpenDb())
                    {
                        var user = db.Single<St_user>("Id={0}", uid);
                        if (user != null)
                            rid = user.RoleId.HasValue ? user.RoleId.Value.ToString() : "";                        
                    }
                }
            }
            return GetRoleName(rid);
        }

        public static IList<St_user> GetUsers()
        {
            using (var db = DBService.OpenDb())
            {
                return db.Select<St_user>();
            }
        }

        public static bool Exists(string username)
        {
            bool res=false;
            if (!string.IsNullOrEmpty(username))
            {
                using (var db = DBService.OpenDb())
                {
                    var u=db.SingleOrDefault<St_user>("LoginName={0}", username);
                    return u != null && u.Id > 0;
                }
            }
            return res;
        }

        public static string GetToken(int userID)
        {
            if(userID>0)
            {
                using (var db = DBService.OpenDb())
                {
                    var u = db.SingleOrDefault<St_user>("Id={0}", userID);
                    return u != null ?(u.LoginToken??""):"";
                }
            }
            return "";
        }

        public static IList<St_user> GetAdminUsers()
        {
            using (var db = DBService.OpenDb())
            {
                return db.Select<St_user>(us=>us.RoleId!=0);
            }
        }

        public static St_user GetUser(int id)
        {
            using (var db = DBService.OpenDb())
            {
                return db.Single<St_user>("Id={0}", id);
            }
        }

        public static string GetUserPerms(int id)
        {
            var perms = "";
            if (id > 0)
            {
                using (var db = DBService.OpenDb())
                {
                    var u = db.Single<St_user>("Id={0}", id);
                    if (u != null && u.Id > 0)
                    {
                        var rid = u.RoleId.HasValue ? u.RoleId.Value : 0;
                        if (rid > 0)
                        {
                            var r = db.Single<St_role>("Id={0}", rid);
                            if (r != null && r.Id > 0)
                            {
                                perms = r.Perms;
                            }
                        }
                    }
                }
            }
            return perms;
        }

        public static int ChangeUserPass(int id, string curpass, string newpass)
        {
            int res = 0;
            if (id > 0 && !string.IsNullOrEmpty(newpass))
            {
                using (var db = DBService.OpenDb())
                {
                    var u = db.Single<St_user>("Id={0}", id);
                    if (u != null && u.Id > 0)
                    {
                        string newp = DBService.SHA1Hash(newpass);
                        string curp = DBService.SHA1Hash(curpass);
                        if (u.LoginPass != curp)
                            res = 1;
                        db.Update<St_user>(new { LoginPass = newp }, us => us.Id == u.Id && us.LoginPass == curp);
                    }
                }
            }
            return res;
        }

        public static void UpdateLoginToken(int id,string curtoken,string newtoken)
        {
            if(id>0)
            {
                using (var db = DBService.OpenDb())
                {
                    db.Update<St_user>(new { LoginToken = newtoken }, us => us.Id == id&&us.LoginToken==curtoken);
                }
            }
        }

        public static void LockUser(int id)
        {
            if (id > 0)
            {
                using (var db = DBService.OpenDb())
                {
                    db.Update<St_user>(new { Status = 9 }, us => us.Id ==id);
                }
            }
        }

        public static void UnLockUser(int id)
        {
            if (id > 0)
            {
                using (var db = DBService.OpenDb())
                {
                    db.Update<St_user>(new { Status = 0 }, us => us.Id == id);
                }
            }
        }

        public static void DelUser(int id)
        {
            if (id > 0)
            {
                using (var db = DBService.OpenDb())
                {
                    var u = db.Single<St_user>("Id={0}", id);
                    if (!u.LoginName.Equals("admin", StringComparison.OrdinalIgnoreCase) && u.RoleId != -1)
                        db.Delete<St_user>(us => us.Id == id);
                }
            }
        }

        public static IList<St_role> GetRoles()
        {
            using (var db = DBService.OpenDb())
            {
                return db.Select<St_role>();
            }
        }

        public static St_role GetRole(int id)
        {
            using (var db = DBService.OpenDb())
            {
                return db.Single<St_role>("Id={0}", id);
            }
        }

        public static void AddRole(string name, IList<string> perms)
        {
            if (!string.IsNullOrEmpty(name) && perms != null && perms.Count > 0)
            {
                using (var db = DBService.OpenDb())
                {
                    var had = db.SingleOrDefault<St_role>("Name={0}", name);
                    if (had == null || had.Id <= 0)
                    {
                        db.Insert<St_role>(new St_role { Name = name, Perms = string.Join(",", perms) });
                    }
                }
            }
        }

        public static void EditRole(int id, string name, IList<string> perms)
        {
            if (id > 0)
            {
                using (var db = DBService.OpenDb())
                {
                    var role = db.Single<St_role>("Id={0}", id);
                    if (role != null && role.Id > 0)
                    {
                        role.Name = name;
                        role.Perms = string.Join(",", perms);
                        db.Update<St_role>(role);
                    }
                }
            }
        }

        public static void DelRole(int id)
        {
            if (id > 0)
            {
                using (var db = DBService.OpenDb())
                {
                    db.Delete<St_role>(rl => rl.Id == id);
                }
            }
        }

        public static St_user CheckUser(string userName, string pass)
        {
            if (!string.IsNullOrEmpty(userName) && !string.IsNullOrEmpty(pass))
            {
                using (var db = DBService.OpenDb())
                {
                    var user = db.SingleOrDefault<St_user>("LoginName={0} AND LoginPass={1} AND Status<5", userName, DBService.SHA1Hash(pass));
                    if (user != null && user.Id > 0)
                    {
                        var claims = new List<Claim>();
                        claims.Add(new Claim(ClaimTypes.Name, userName));
                        claims.Add(new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()));
                        claims.Add(new Claim("http://schemas.microsoft.com/accesscontrolservice/2010/07/claims/identityprovider", "ASP.NET Identity"));
                        int rid = 0;
                        claims.Add(new Claim(ClaimTypes.Role, rid.ToString()));
                        claims.Add(new Claim("Sex", user.Sex.HasValue && user.Sex.Value ? "1" : "0"));
                        claims.Add(new Claim("DisplayName", user.Name));
                        return user;
                    }
                }
            }

            return null;
        }
    }
}
