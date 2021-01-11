using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace Web
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.MapRoute(
                name: "MyRoute",
                url: "{controller}/{action}/{id}/{subid}",
                defaults: new { controller = "My", action = "Index", id = UrlParameter.Optional, subid = UrlParameter.Optional }
            );

            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "My", action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}
