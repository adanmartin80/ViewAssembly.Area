using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ViewAssembly.Area.Areas.AnalisisRiesgo.Models;

namespace ViewAssembly.Area.Areas.AnalisisRiesgo.Controllers
{
    [RoutePrefix("AnalisisDeRiesgo")]
    public class ARController : Controller
    {
        // GET: AR
        public ActionResult Index()
        {
            var viewModel = new ARViewModel
            {
                Titulo = "Prueba de ViewModel",
                Mensaje = "Creado mediante template"
            };

            return View(viewModel);
        }
    }
}