using System.Web;
using System.Web.Hosting;
using System.Web.Mvc;
using System.Web.Routing;
using System.Web.WebPages;
using ViewsAssembly.ViewsControllers;

[assembly: WebActivatorEx.PostApplicationStartMethod(typeof(ViewAssembly.Area.AnalisisDeRiesgoMvcStart), "Start")]

namespace ViewAssembly.Area {
    public static class AnalisisDeRiesgoMvcStart {
        public static void Start() {
            var assembly = typeof(AssemblyVirtualPathProvider).Assembly;
            HostingEnvironment.RegisterVirtualPathProvider(new AssemblyVirtualPathProvider(assembly));
            RouteConfig.RegisterRoutes(RouteTable.Routes);
        }
    }
}
