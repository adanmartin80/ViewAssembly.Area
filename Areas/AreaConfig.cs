using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Web;
using System.Web.Hosting;
using System.Web.Mvc;
using System.Web.Routing;
using ViewsAssembly.ViewsControllers;

namespace ViewAssembly.Area.Areas
{
    public class AreaConfig : AreaRegistration
    {
        public override string AreaName => "AnalisisRiesgo";

        public override void RegisterArea(AreaRegistrationContext context)
        {
            IgnoreControllers(context.Routes, typeof(AreaConfig).Assembly);
        }

        /// <summary>
        /// Ignoramos todas las rutas que se autogeneran en plan hhtp://localhost:8080/{controlador}
        /// para que solo existan las rutas de areas y las del controlador Global.
        /// </summary>
        /// <param name="routes"></param>
        /// <param name="assembly"></param>
        private static void IgnoreControllers(RouteCollection routes, Assembly assembly)
        {
            var controllers = assembly.GetTypes().Where(x => x.IsClass && x.BaseType == typeof(Controller) && !x.FullName.Contains("GlobalControllers"));
            controllers.ToList().ForEach(x => routes.IgnoreRoute($"{x.Name.Replace("Controller", "")}/"));
        }

    }
}
