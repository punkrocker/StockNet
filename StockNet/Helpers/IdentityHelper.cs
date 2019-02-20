using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Security.Principal;
using System.Security.Claims;
using StockNet.Service;

namespace StockNet.Helpers
{
    public static class IdentityHelper
    {
        public static string GetDisplayName(IIdentity identity)
        {
            ClaimsIdentity ci = identity as ClaimsIdentity;
            if (ci != null)
            {
                var claim = ci.Claims.SingleOrDefault(cl => cl.Type == "DisplayName");
                if (claim != null)
                    return claim.Value;
            }
            return identity.Name;
        }

        public static bool GetSex(IIdentity identity)
        {
            bool sex = false;
            ClaimsIdentity ci = identity as ClaimsIdentity;
            if (ci != null)
            {
                var claim = ci.Claims.SingleOrDefault(cl => cl.Type == "Sex");
                if (claim != null && claim.Value == "1")
                    sex = true;
            }
            return sex;
        }

        public static string GetRoleName(IIdentity identity)
        {
            string uid="";
            ClaimsIdentity ci = identity as ClaimsIdentity;
            if (ci != null)
            {
                var claim = ci.Claims.SingleOrDefault(cl => cl.Type == ClaimTypes.NameIdentifier);
                if (claim != null)
                    uid = claim.Value;
            }
            return UserService.GetRoleNameByUserID(uid);
        }
    }
}