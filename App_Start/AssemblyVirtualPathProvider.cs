using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.Remoting;
using System.Text;
using System.Threading.Tasks;
using System.Web.Caching;
using System.Web.Hosting;
using System.Web.Mvc;
using System.Web.UI;

namespace ViewsAssembly.ViewsControllers
{
    public class AssemblyVirtualPathProvider : VirtualPathProvider
 {
     private readonly Assembly assembly;
     private readonly IEnumerable<VirtualPathProvider> providers;
  
     public AssemblyVirtualPathProvider(Assembly assembly)
       {
         var engines = ViewEngines.Engines.OfType<VirtualPathProviderViewEngine>().ToList();
  
         this.providers = engines.Select(x => x.GetType().GetProperty("VirtualPathProvider", BindingFlags.NonPublic | BindingFlags.Instance).GetValue(x, null) as VirtualPathProvider).Distinct().ToList();
         this.assembly = assembly;
     }
     public override CacheDependency GetCacheDependency(String virtualPath, IEnumerable virtualPathDependencies, DateTime utcStart)
     {
         if (this.FindFileByPath(this.CorrectFilePath(virtualPath)) != null)
         {
                return (new AssemblyCacheDependency(assembly));
            }
      else
      {
                return (base.GetCacheDependency(virtualPath, virtualPathDependencies, utcStart));
            }
     }
  
     public override Boolean FileExists(String virtualPath)
     {
         foreach (var provider in this.providers)
         {
            if (provider.FileExists(virtualPath) == true)
                {
                    return (true);
                }
        }
  
         var exists = this.FindFileByPath(this.CorrectFilePath(virtualPath)) != null;
  
         return (exists);
     }
  
     public override VirtualFile GetFile(String virtualPath)
     {
         var resource = null as Stream;
  
         foreach (var provider in this.providers)
         {
          var file = provider.GetFile(virtualPath);
  
    if (file != null)
              {
                  try
        {
                       resource = file.Open();
                       return (file);
                   }
               catch
     {
                   }
           }
          }
  
         var resourceName = this.FindFileByPath(this.CorrectFilePath(virtualPath));
    
         return (new AssemblyVirtualFile(virtualPath, this.assembly, resourceName));
        }

        public override string CombineVirtualPaths(string basePath, string relativePath)
        {
            return base.CombineVirtualPaths(basePath, relativePath);
        }
        public override ObjRef CreateObjRef(Type requestedType)
        {
            return base.CreateObjRef(requestedType);
        }
        public override string GetCacheKey(string virtualPath)
        {
            return base.GetCacheKey(virtualPath);
        }
        public override bool DirectoryExists(string virtualDir)
        {
            return base.DirectoryExists(virtualDir);
        }
        public override VirtualDirectory GetDirectory(string virtualDir)
        {
            return base.GetDirectory(virtualDir);
        }
        public override string GetFileHash(string virtualPath, IEnumerable virtualPathDependencies)
        {
            return base.GetFileHash(virtualPath, virtualPathDependencies);
        }
        protected String FindFileByPath(String virtualPath)
        {
            var resourceNames = this.assembly.GetManifestResourceNames();
            if (virtualPath.Contains("**")) // acepta comodines: ensamblado.**.fichero
            {
                var virtualPaths = virtualPath.Split(new[] { "**" }, StringSplitOptions.RemoveEmptyEntries);
                return (resourceNames.SingleOrDefault(r => virtualPaths.All(p => r.Contains(p)) && r.EndsWith(virtualPaths.LastOrDefault(), StringComparison.OrdinalIgnoreCase)));
            }

            return (resourceNames.SingleOrDefault(r => r.EndsWith(virtualPath, StringComparison.OrdinalIgnoreCase)));
        }
  
     protected String CorrectFilePath(String virtualPath)
     {
         return $"{(virtualPath.Replace("~", String.Empty).Replace('/', '.'))}";
     }
 }
  
 public class AssemblyVirtualFile : VirtualFile
 {
     private readonly Assembly assembly;
     private readonly String resourceName;
     private readonly bool? isViewStart;
        public string ResourceName => resourceName;

        public AssemblyVirtualFile(String virtualPath, Assembly assembly, String resourceName) : base(virtualPath)
     {
         this.assembly = assembly;
         this.resourceName = resourceName;
         this.isViewStart = resourceName?.EndsWith("_ViewStart.cshtml");
     }
  
     public override Stream Open()
     {
            string usingString = $"@using System.Web.Mvc{Environment.NewLine}@using System.Web.Mvc.Ajax{Environment.NewLine}@using System.Web.Mvc.Html{Environment.NewLine}@using System.Web.Optimization{Environment.NewLine}@using System.Web.Routing{ Environment.NewLine}";
            const string starInherit = "@inherits System.Web.WebPages.StartPage";
            const string inherit = "@inherits System.Web.Mvc.WebViewPage<dynamic>";
            var stream = this.assembly.GetManifestResourceStream(this.resourceName);
            var memoryStream = new MemoryStream();
            var read = new StreamReader(stream);
            var content = read.ReadToEnd();
            var write = new StreamWriter(memoryStream);
            write.Write($"{(isViewStart.HasValue && isViewStart.Value ? starInherit : inherit)}{Environment.NewLine}{usingString}{content}");
            write.Flush();
            memoryStream.Position = 0;

            return memoryStream;
     }

        //private async Task<string> RenderPartialViewToString(string viewName, object model)
        //{
        //    if (string.IsNullOrEmpty(viewName))
        //        viewName = ControllerContext.ActionDescriptor.ActionName;

        //    ViewData.Model = model;

        //    using (var writer = new StringWriter())
        //    {
        //        ViewEngineResult viewResult =
        //            _viewEngine.FindView(ControllerContext, viewName, false);

        //        ViewContext viewContext = new ViewContext(
        //            ControllerContext,
        //            viewResult.View,
        //            ViewData,
        //            TempData,
        //            writer,
        //            new HtmlHelperOptions()
        //        );

        //        await viewResult.View.RenderAsync(viewContext);

        //        return writer.GetStringBuilder().ToString();
        //    }
        //}
    }
  
 public class AssemblyCacheDependency : CacheDependency
 {
     private readonly Assembly assembly;
  
     public AssemblyCacheDependency(Assembly assembly)
     {
         this.assembly = assembly;
         this.SetUtcLastModified(File.GetCreationTimeUtc(assembly.Location));
     }
 }
}
