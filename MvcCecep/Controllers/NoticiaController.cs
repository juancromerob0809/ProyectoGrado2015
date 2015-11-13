using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data.Entity;
using System.Data;


namespace MvcCecep.Controllers
{
    public class NoticiaController : Controller
    {
        private CecepEntities db = new CecepEntities();

        // GET: Noticia
        public ActionResult Index()
        {
            var modelo = db.ccnoticia.ToList();

            return View(modelo);
        }       

        // GET: Noticia/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Noticia/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(ccnoticia ccnoticia)
        {
            try
            {
                db.ccnoticia.Add(ccnoticia);
                db.SaveChanges();

                return RedirectToAction("Index");
            }
            catch
            {
                return View(ccnoticia);
            }
        }

        // GET: Noticia/Edit/5
        public ActionResult Edit(int id)
        {
            var modelo = db.ccnoticia.Find(id);

            return View(modelo);
        }

        // POST: Noticia/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(ccnoticia ccnoticia)
        {
            try
            {
                db.Entry(ccnoticia).State = EntityState.Modified;
                db.SaveChanges();

                return RedirectToAction("Index");
            }
            catch
            {
                return View(ccnoticia);
            }
        }

        // GET: Noticia/Delete/5
        public ActionResult Delete(int id)
        {
            var modelo = db.ccnoticia.Find(id);

            return View(modelo);
        }

        // POST: Noticia/Delete/5
        [HttpPost,ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            var ccnoticia = db.ccnoticia.Find(id);
            try
            {
                db.ccnoticia.Remove(ccnoticia);
                db.SaveChanges();

                return RedirectToAction("Index");
            }
            catch
            {
                return View(ccnoticia);
            }
        }
    }
}
