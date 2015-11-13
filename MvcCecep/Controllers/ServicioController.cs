using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data.Entity;
using System.Data;

namespace MvcCecep.Controllers
{
    public class ServicioController : Controller
    {
        private CecepEntities db = new CecepEntities();
        // GET: Servicio
        public ActionResult Index()
        {
            var modelo = db.cctiposerv.ToList();

            return View(modelo);
        }

        // GET: Servicio/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: Servicio/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Servicio/Create
        [HttpPost]
        public ActionResult Create(cctiposerv cctiposerv)
        {
            if (ModelState.IsValid)
            {
                db.cctiposerv.Add(cctiposerv);
                db.SaveChanges();

                return RedirectToAction("Edit", new { id = cctiposerv.cctiposervid });
            }

            return View(cctiposerv);
        }

        // GET: Servicio/Edit/5
        public ActionResult Edit(int id)
        {
            var modelo = db.cctiposerv.Find(id);

            return View(modelo);
        }

        // POST: Servicio/Edit/5
        [HttpPost]
        public ActionResult Edit(cctiposerv cctiposerv)
        {

            if (ModelState.IsValid)
            {
                db.Entry(cctiposerv).State = EntityState.Modified;
                db.SaveChanges();

                return RedirectToAction("Index");
            }

            return View();

        }

        // GET: Servicio/Delete/5
        public ActionResult Delete(int id)
        {
            var modelo = db.cctiposerv.Find(id);

            return View(modelo);
        }

        // POST: Servicio/Delete/5
        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(int id)
        {
            var modelo = db.cctiposerv.Find(id);

            try
            {
                db.cctiposerv.Remove(modelo);
                db.SaveChanges();

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }


        #region Detalle

        public ActionResult CreateDetalle(int id)
        {
            cctiposervdet cctiposervdet = new cctiposervdet();
            cctiposervdet.cctiposervid = id;

            ViewBag.PossibleCategorias = db.cctiposervcat.ToList();


            return View(cctiposervdet);
        }

        [HttpPost]
        public ActionResult CreateDetalle(cctiposervdet cctiposervdet)
        {
            if (ModelState.IsValid)
            {
                db.cctiposervdet.Add(cctiposervdet);
                db.SaveChanges();

                return RedirectToAction("Edit", new { id = cctiposervdet.cctiposervid });
            }

            ViewBag.PossibleCategorias = db.cctiposervcat.ToList();

            return View(cctiposervdet);
        }

        public ActionResult EditDetalle(int id)
        {
            cctiposervdet cctiposervdet = db.cctiposervdet.Find(id);

           
            ViewBag.PossibleCategorias = db.cctiposervcat.ToList();

            return View(cctiposervdet);
        }

        [HttpPost]
        public ActionResult EditDetalle(cctiposervdet cctiposervdet)
        {
            if (ModelState.IsValid)
            {
                db.Entry(cctiposervdet).State = EntityState.Modified;
                db.SaveChanges();

                return RedirectToAction("Edit", new { id = cctiposervdet.cctiposervid });
            }

            ViewBag.PossibleCategorias = db.cctiposervcat.ToList();

            return View(cctiposervdet);
        }

        #endregion



    }
}
