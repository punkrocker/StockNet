using System;
using Microsoft.Owin;
using Microsoft.Owin.Security.Cookies;
using Owin;
using StockNet.Models;

namespace StockNet
{
    public partial class Startup
    {
        // 有关配置身份验证的详细信息，请访问 http://go.microsoft.com/fwlink/?LinkId=301864
        public void ConfigureAuth(IAppBuilder app)
        {
            app.UseCookieAuthentication(new CookieAuthenticationOptions
            {
                AuthenticationType = "MyClaimsLogin",
                LoginPath = new PathString("/Account/Login")
            }); 
        }
    }
}