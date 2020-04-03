using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace ViewAssembly.Area
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.MapMvcAttributeRoutes();
            //routes.MapRoute(
            //  name: "ResourceRoute",
            //  url: "{controller}/{extension}/{resourceName}",
            //  defaults: new { controller = "ResourceRouteController", action = "Get", extension = UrlParameter.Optional, resourceName = UrlParameter.Optional },
            //  new { httpMethod = new HttpMethodConstraint("GET") },
            //  namespaces: new[] { "ViewAssembly.Area.GlobalControllers" }
            //  );
        }
    }
}
