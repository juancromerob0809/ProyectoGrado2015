using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MvcCecep.Controllers
{
    public class HomeController : Controller
    {
        private CecepEntities db = new CecepEntities();

        public ActionResult Index()
        {
            ViewBag.Noticias = db.ccnoticia.ToList();
            ViewBag.Eventos = db.ccevento.ToList();
            ViewBag.Cursos = db.cccurso.ToList();


            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your app description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }

        public ActionResult Noticia(int id)
        {
            ccnoticia modelo = db.ccnoticia.Find(id);

            return View(modelo);
        }

    }
}
