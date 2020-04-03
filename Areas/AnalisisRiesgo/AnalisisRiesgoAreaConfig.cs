using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Hosting;
using System.Web.Mvc;
using System.Web.Routing;
using ViewsAssembly.ViewsControllers;

namespace ViewAssembly.Area.Areas.AnalisisRiesgo
{
    public class AnalisisRiesgoAreaConfig : AreaRegistration
    {
        public override string AreaName => "AnalisisRiesgo";

        public override void RegisterArea(AreaRegistrationContext context)
        {
            context.MapRoute(name: AreaName, url: $"{AreaName}/{{controller}}/{{action}}/{{id}}",
                defaults: new { controller = "AR", action = "Index", id = UrlParameter.Optional },
                new[] { "ViewAssembly.Area.Areas.AnalisisRiesgo.Controllers" });
        }
    }
}
