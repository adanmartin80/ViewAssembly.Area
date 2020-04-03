using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Hosting;
using System.Web.Mvc;

namespace ViewAssembly.Area.GlobalControllers
{
    [RoutePrefix("Content")]
    public class ResourceRouteController : Controller
    {
        [Route("Resource/{area}/{extension}/{resourceName}")]
        public ActionResult Index(string area, string extension, string resourceName)
        {
            resourceName = $"/{area}/Content/**/{resourceName}/{extension}";
            var exist = HostingEnvironment.VirtualPathProvider.FileExists(resourceName);
            if (!exist)
                return new EmptyResult();

            var virtualFile = HostingEnvironment.VirtualPathProvider.GetFile(resourceName) as ViewsAssembly.ViewsControllers.AssemblyVirtualFile;
            if (virtualFile == null)
                return new EmptyResult();
  
            var path = virtualFile.ResourceName;
            var contentType = System.Web.MimeMapping.GetMimeMapping(path);
            var resource = this.GetType().Assembly.GetManifestResourceStream(path);
            return File(resource, contentType);
        }
    }
}