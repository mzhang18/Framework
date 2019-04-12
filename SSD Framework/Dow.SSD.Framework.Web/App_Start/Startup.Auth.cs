using System;
using System.Collections.Generic;
using System.Configuration;
using System.Globalization;
using System.IdentityModel.Claims;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using Microsoft.Owin.Security;
using Microsoft.Owin.Security.Cookies;
using Microsoft.Owin.Security.OpenIdConnect;
using Microsoft.IdentityModel.Clients.ActiveDirectory;
using Owin;


namespace Dow.SSD.Framework.Web
{
    public partial class Startup
    {
        public void ConfigureAuth(IAppBuilder app)
        {
            app.SetDefaultSignInAsAuthenticationType(CookieAuthenticationDefaults.AuthenticationType);
            app.UseCookieAuthentication(new CookieAuthenticationOptions()
            {
                CookieName = "cloudrem",
                CookieSecure = CookieSecureOption.Never,
                SlidingExpiration = true,
                CookieHttpOnly = true,
                ExpireTimeSpan = System.TimeSpan.FromMinutes(480)
            });

            app.UseOpenIdConnectAuthentication(
              new OpenIdConnectAuthenticationOptions
              {
                  ClientId = ConfigurationManager.AppSettings["ClientId"],
                  Authority = ConfigurationManager.AppSettings["Authority"],
                  SignInAsAuthenticationType = "Cookies",
                  UseTokenLifetime = false,
                  AuthenticationMode = AuthenticationMode.Passive,
                  Notifications = new OpenIdConnectAuthenticationNotifications
                  {
                      AuthenticationFailed = (context) =>
                      {
                          context.HandleResponse();
                          context.Response.Redirect("/Error/Index?message=" + context.Exception.Message);
                          return Task.FromResult(0);
                      },
                      RedirectToIdentityProvider = (context) =>
                      {
                          context.ProtocolMessage.DomainHint = ConfigurationManager.AppSettings["DomainHint"];
                          return Task.FromResult(0);
                      },
                  }
              });
        }
    }
}
