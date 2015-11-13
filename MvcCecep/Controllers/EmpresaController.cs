using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using MvcCecep;
using System.Web.Security;

namespace MvcCecep.Controllers
{
    public class EmpresaController : Controller
    {
        private CecepEntities db = new CecepEntities();

        // GET: Empresa
        public ActionResult Index()
        {
            var ccempresa = db.ccempresa.Include(c => c.ccarea);
            return View(ccempresa.ToList());
        }

        // GET: Empresa/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ccempresa ccempresa = db.ccempresa.Find(id);
            if (ccempresa == null)
            {
                return HttpNotFound();
            }
            return View(ccempresa);
        }

        // GET: Empresa/Create
        public ActionResult Create()
        {
            ViewBag.ccareaid = new SelectList(db.ccarea, "ccareaid", "codigo");
            return View();
        }

        // POST: Empresa/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(ccempresa ccempresa, string contra2)
        {
            ccempresa.fechacreacion = DateTime.Now;


            if (ccempresa.contraseña != contra2)
            {
                ModelState.AddModelError("contraseña", "Las contraseñas no coinciden por favor verifique");
            }

            try
            {
                var emp = db.ccempresa.Where(x => x.loggin == ccempresa.loggin);

                if (emp.Count() > 0)
                {
                    ModelState.AddModelError("loggin", "Este usuario ya esta en uso");
                }

            }
            catch { }

            try
            {
                var emp = db.ccempresa.Where(x => x.nit == ccempresa.nit);
                if (emp.Count() > 0)
                {
                    ModelState.AddModelError("nit", "Este Nit ya ha sido registrado con anterioridad, por favor verifique");
                }
            }
            catch { }


            if (ModelState.IsValid)
            {
                db.ccempresa.Add(ccempresa);
                db.SaveChanges();

                return RedirectToAction("Login", "Account");
            }


            ViewBag.ccareaid = new SelectList(db.ccarea, "ccareaid", "codigo", ccempresa.ccareaid);


            return View(ccempresa);
        }

        // GET: Empresa/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ccempresa ccempresa = db.ccempresa.Find(id);
            if (ccempresa == null)
            {
                return HttpNotFound();
            }
            ViewBag.ccareaid = new SelectList(db.ccarea, "ccareaid", "codigo", ccempresa.ccareaid);
            return View(ccempresa);
        }

        // POST: Empresa/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ccempresaid,nit,dv,descripcion,direccion,telefono,celular,contactnombre,contacttelefono,contactcelular,loggin,contraseña,fechafundacion,ccareaid,fechacreacion")] ccempresa ccempresa)
        {
            if (ModelState.IsValid)
            {
                db.Entry(ccempresa).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.ccareaid = new SelectList(db.ccarea, "ccareaid", "codigo", ccempresa.ccareaid);
            return View(ccempresa);
        }

        // GET: Empresa/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ccempresa ccempresa = db.ccempresa.Find(id);
            if (ccempresa == null)
            {
                return HttpNotFound();
            }
            return View(ccempresa);
        }

        // POST: Empresa/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            ccempresa ccempresa = db.ccempresa.Find(id);
            db.ccempresa.Remove(ccempresa);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
