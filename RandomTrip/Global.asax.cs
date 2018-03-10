using Google.Maps;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Optimization;
using System.Web.Routing;
using System.Web.Security;
using System.Web.SessionState;

namespace RandomTrip
{
    public class Global : HttpApplication
    {
        void Application_Start(object sender, EventArgs e)
        {
            // Code that runs on application startup
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
            GoogleSigned.AssignAllServices(new GoogleSigned("AIzaSyCxOXK6QIQRnC5NxEl0BqQ20XqwkVVbdK0"));
        }
    }
}