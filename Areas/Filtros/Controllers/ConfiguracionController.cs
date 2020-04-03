using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ViewAssembly.Area.Areas.Filtros.Controllers
{
    public class ConfiguracionController : Controller
    {
        // GET: Filtros/Filtros
        public ActionResult Index()
        {
            return View("Configuracion");
        }
    }
}