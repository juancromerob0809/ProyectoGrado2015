using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data;
using System.Data.Entity;


namespace MvcCecep.Controllers
{
    public class EventoController : Controller
    {
        private CecepEntities db = new CecepEntities();

        // GET: Evento
        public ActionResult Index()
        {
            var modelo = db.ccevento.ToList();

            return View(modelo);
        }

        // GET: Evento/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Evento/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(ccevento ccevento)
        {
            try
            {
                db.ccevento.Add(ccevento);
                db.SaveChanges();

                return RedirectToAction("Index");
            }
            catch
            {
                return View(ccevento);
            }
        }

        // GET: Evento/Edit/5
        public ActionResult Edit(int id)
        {
            var modelo = db.ccevento.Find(id);

            return View(modelo);
        }

        // POST: Evento/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(ccevento ccevento)
        {
            try
            {
                db.Entry(ccevento).State = EntityState.Modified;
                db.SaveChanges();

                return RedirectToAction("Index");
            }
            catch
            {
                return View(ccevento);
            }
        }

        // GET: Evento/Delete/5
        public ActionResult Delete(int id)
        {
            var modelo = db.ccevento.Find(id);

            return View(modelo);
        }

        // POST: Evento/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            var ccevento = db.ccevento.Find(id);
            try
            {
                db.ccevento.Remove(ccevento);
                db.SaveChanges();

                return RedirectToAction("Index");
            }
            catch
            {
                return View(ccevento);
            }
        }
    }
}
