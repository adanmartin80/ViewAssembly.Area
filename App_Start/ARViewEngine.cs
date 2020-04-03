using System.Web.Mvc;

namespace ViewAssembly.Area
{
    public class ARViewEngine : RazorViewEngine
    {
        public ARViewEngine()
        {
            ViewLocationFormats = new string[]
            {
            "~/Areas/AnalisisRiesgo/Views/{1}/{0}.cshtml"
            };
        }
    }
}
