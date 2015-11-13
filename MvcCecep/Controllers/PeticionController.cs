using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data.Entity;
using System.Data;
using MvcCecep.Models;


namespace MvcCecep.Controllers
{
    public class PeticionController : Controller
    {
        private CecepEntities db = new CecepEntities();
        // GET: Peticion


        public ActionResult Index()
        {
            int ccempresaid = Convert.ToInt32(Session["company"]);

            var modelo = db.ccpeticion.Include(x => x.ccarea).Where(x => x.ccempresaid == ccempresaid);

            ViewBag.Ccempresaid = ccempresaid;


            return View(modelo);
        }

        #region Encabezado

        public ActionResult Create(int ccempresaid)
        {

            ViewBag.cctiposervicio = db.cctiposerv;
            ViewBag.ccarea = db.ccarea;

            ccpeticion modelo = new ccpeticion();

            modelo.ccempresaid = ccempresaid;
            modelo.fechacre = DateTime.Now;

            return View(modelo);
        }

        [HttpPost]
        public ActionResult Create(ccpeticion ccpeticion)
        {

            ccpeticion.fechains = DateTime.Now;
            ccpeticion.activa = true;

            if (ModelState.IsValid)
            {
                db.ccpeticion.Add(ccpeticion);
                db.SaveChanges();
            }

            ViewBag.cctiposervicio = db.cctiposerv;
            ViewBag.ccarea = db.ccarea;

            return RedirectToAction("Edit", new { id = ccpeticion.ccpeticionid });
        }


        public ActionResult Edit(int id)
        {
            ViewBag.cctiposervicio = db.cctiposerv;
            ViewBag.ccarea = db.ccarea;

            var modelo = db.ccpeticion.Find(id);

            return View(modelo);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(ccpeticion ccpeticion)
        {
            ccpeticion.activa = true;

            if (ModelState.IsValid)
            {
                db.Entry(ccpeticion).State = EntityState.Modified;
                db.SaveChanges();

                return RedirectToAction("Index", new { ccempresaid = ccpeticion.ccempresaid });
            }

            ViewBag.cctiposervicio = db.cctiposerv;
            ViewBag.ccarea = db.ccarea;

            return View(ccpeticion);
        }

        #endregion


        #region Detalle

        public ActionResult CreateDetalle(int ccpeticionid)
        {
            ViewBag.cctiposervicio = db.cctiposerv;
            ViewBag.ccarea = db.ccarea;

            ccpeticiondet modelo = new ccpeticiondet();
            modelo.ccpeticionid = ccpeticionid;

            return View(modelo);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CreateDetalle(ccpeticiondet ccpeticiondet)
        {

            if (ModelState.IsValid)
            {
                db.ccpeticiondet.Add(ccpeticiondet);
                db.SaveChanges();
                return RedirectToAction("Edit", new { id = ccpeticiondet.ccpeticiondetid });
            }

            ViewBag.cctiposervicio = db.cctiposerv;
            ViewBag.ccarea = db.ccarea;

            return View(ccpeticiondet);
        }



        public ActionResult EditDetalle(int ccpeticiondetid)
        {
            ccpeticiondet modelo = db.ccpeticiondet.Find(ccpeticiondetid);

            VerificaServicio(ccpeticiondetid);

            var Verificaciones = db.ccpeticionserv.Where(x => x.ccpeticiondetid == modelo.ccpeticiondetid).ToList();

            PeticionVerificacion PeticionVerificacion = new Models.PeticionVerificacion();
            PeticionVerificacion.ccpeticionid = modelo.ccpeticionid;
            PeticionVerificacion.ccpeticiondetid = modelo.ccpeticiondetid;
            PeticionVerificacion.cctiposervid = Convert.ToInt32(modelo.cctiposervid);
            PeticionVerificacion.fecha = Convert.ToDateTime(modelo.fecha);
            PeticionVerificacion.descripcion = modelo.descripcion;
            PeticionVerificacion.ccpetecionservid = Verificaciones.Select(x => x.ccpeticionservid).ToArray();
            PeticionVerificacion.cctiposervdetid = Verificaciones.Select(x => x.cctiposerviciodetid).ToArray();
            PeticionVerificacion.cctiposerv_codigo = Verificaciones.Select(x => x.cctiposervdet.codigo).ToArray();
            PeticionVerificacion.cctiposervdet_descrip = Verificaciones.Select(x => x.cctiposervdet.descripcion).ToArray();
            PeticionVerificacion.estado = Verificaciones.Select(x => x.estado).ToArray();



            ViewBag.cctiposervicio = db.cctiposerv;
            ViewBag.ccarea = db.ccarea;

            return View(PeticionVerificacion);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult EditDetalle(PeticionVerificacion PeticionVerificacion)
        {

            var modelo = db.ccpeticiondet.Find(PeticionVerificacion.ccpeticiondetid);
            modelo.descripcion = PeticionVerificacion.descripcion;
            modelo.fecha = PeticionVerificacion.fecha;
            modelo.cctiposervid = PeticionVerificacion.cctiposervid;

            db.Entry(modelo).State = EntityState.Modified;
            db.SaveChanges();


            for (int i = 0; i < PeticionVerificacion.cctiposerv_codigo.Length; i++)
            {
                var aux = PeticionVerificacion.ccpetecionservid[i];

                ccpeticionserv ccpeticionserv = db.ccpeticionserv.Find(aux);

                bool estado = Convert.ToBoolean(PeticionVerificacion.estado[i]);

                ccpeticionserv.estado = estado;

                db.Entry(ccpeticionserv).State = EntityState.Modified;
                db.SaveChanges();
            }

            ViewBag.cctiposervicio = db.cctiposerv;
            ViewBag.ccarea = db.ccarea;

            return RedirectToAction("Edit", new { id = PeticionVerificacion.ccpeticionid });
        }

        public ActionResult DeleteDetalle(int ccpeticiondetid)
        {
            ccpeticiondet modelo = db.ccpeticiondet.Find(ccpeticiondetid);

            return View(modelo);
        }


        [HttpPost, ActionName("DeleteDetalle")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteDetalleConfirmed(int ccpeticiondetid)
        {
            ccpeticiondet modelo = db.ccpeticiondet.Find(ccpeticiondetid);

            int aux = modelo.ccpeticionid;

            db.ccpeticiondet.Remove(modelo);
            db.SaveChanges();

            return RedirectToAction("Edit", new { id = aux });
        }

        #endregion


        #region Servicio

        public void VerificaServicio(int id)
        {
            var ccpeticiondet = db.ccpeticiondet.Find(id);

            var requisitos = db.cctiposervdet.Where(x => x.cctiposervid == ccpeticiondet.cctiposervid).ToList();

            foreach (var item in requisitos)
            {
                try
                {
                    var existe = db.ccpeticionserv.Single(x => x.ccpeticiondetid == ccpeticiondet.ccpeticiondetid && x.cctiposerviciodetid == item.cctiposervdetid);
                }
                catch
                {
                    ccpeticionserv ccpeticionserv = new ccpeticionserv();
                    ccpeticionserv.ccpeticiondetid = ccpeticiondet.ccpeticiondetid;
                    ccpeticionserv.cctiposerviciodetid = item.cctiposervdetid;
                    ccpeticionserv.estado = false;

                    db.ccpeticionserv.Add(ccpeticionserv);
                    db.SaveChanges();
                }
            }


        }

        #endregion


        public ActionResult Grafico(int id)
        {
            var modelo = db.ccpeticiondet.Find(id);

            var detalles = db.ccpeticionserv.Where(x => x.ccpeticiondetid == modelo.ccpeticiondetid).ToList();

            List<GraficoModel> Listado = new List<GraficoModel>();

            var Categorias = db.cctiposervcat.ToList();


            foreach (var item in Categorias)
            {
                GraficoModel det = new GraficoModel();
                det.categoria = item.descripcion;
                det.Aprobados= detalles.Where(x => x.cctiposervdet.categoria == item.descripcion && x.estado == true).Count();

                Listado.Add(det);
            }

            GraficoModelVista graf = new GraficoModelVista();
            graf.categoria = Listado.Select(x => x.categoria).ToArray();
            graf.Aprobados = Listado.Select(x => x.Aprobados).ToArray();

            ViewBag.GraficoCategorias = Listado.Select(x => x.categoria).ToArray();
            ViewBag.GraficoValores = Listado.Select(x => x.Aprobados).ToArray();

            ViewBag.GraficoModel = graf;


            return View(graf);
        }

    }
}