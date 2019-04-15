using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using Microsoft.Owin.Security.Cookies;
using Microsoft.Owin.Security.OpenIdConnect;
using Microsoft.Owin.Security;

namespace Dow.SSD.Framework.Web
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
        }
        protected void Application_PreRequestHandlerExecute(object sender, EventArgs e)
        {
            try
            {
                if (User.Identity.IsAuthenticated == false)
                {                  
                    Context.GetOwinContext().Authentication.Challenge(new AuthenticationProperties { RedirectUri = Request.Url.AbsoluteUri },
                     OpenIdConnectAuthenticationDefaults.AuthenticationType);               
                }
            }
            catch (Exception ex)
            {
                //Handle Exception                
            }
        }
    }
}
