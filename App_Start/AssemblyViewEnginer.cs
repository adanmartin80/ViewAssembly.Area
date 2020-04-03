using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using System.Web.SessionState;

namespace ViewsAssembly.ViewsControllers
{
    public class AssemblyViewEnginer: System.Web.Mvc.MvcWebRazorHostFactory
    {
        private AssemblyVirtualPathProvider _pathProvider;
        public AssemblyViewEnginer(AssemblyVirtualPathProvider pathProvider)
        {
            _pathProvider = pathProvider;
            
        }

    }
}