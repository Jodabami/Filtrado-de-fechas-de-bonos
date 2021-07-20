using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Bonos.Models;
using Bonos.Models.ViewModels;

namespace Bonos.Controllers
{
    public class tblBonosController : Controller
    {
        private BonosEntities db = new BonosEntities();

        // GET: tblBonos
        public ActionResult Index()
        {
            var tblBonos = db.tblBonos.Include(t => t.tblUsuarios);
            return View(tblBonos.ToList());
        }

        [HttpPost]
        public ActionResult Index(DateTime Desde, DateTime Hasta)
        {
            List<tblBonos> lista1 = db.tblBonos.OrderBy(m => m.IdUsuarios).ToList();
            List<tblBonos> lista2 = new List<tblBonos>();
            List<SumaBonos> lista3 = new List<SumaBonos>();
            int aux1 = 0;
            int aux2 = -1;
            int primeraFecha = 0;
            int segundaFecha = 0;

            foreach (var a in lista1)
            {
                primeraFecha = DateTime.Compare(Desde, a.Fecha);
                segundaFecha = DateTime.Compare(Hasta, a.Fecha);
                if ((primeraFecha == -1 || primeraFecha == 0) && (segundaFecha == 1 || segundaFecha == 0))
                {
                    lista2.Add(db.tblBonos.Where(p => p.IdBonos == a.IdBonos).First());
                }
            }

            foreach (var d in lista2)
            {
                if (d.IdUsuarios == aux1)
                {
                    lista3[aux2].Bonos += d.Bono;
                }
                else
                {
                    lista3.Add(new SumaBonos()
                    {
                        Nombre = db.tblUsuarios.Where(p => p.IdUsuarios == d.IdUsuarios).First().Nombre,
                        Bonos = d.Bono
                    });

                    aux2++;
                    aux1 = d.IdUsuarios;
                }
            }
            return View("ListaBonos", lista3);
        }

        public ActionResult ListaBonos()
        {
            return View();
        }

        // GET: tblBonos/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tblBonos tblBonos = db.tblBonos.Find(id);
            if (tblBonos == null)
            {
                return HttpNotFound();
            }
            return View(tblBonos);
        }

        // GET: tblBonos/Create
        public ActionResult Create()
        {
            ViewBag.IdUsuarios = new SelectList(db.tblUsuarios, "IdUsuarios", "Nombre");
            return View();
        }

        // POST: tblBonos/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que quiere enlazarse. Para obtener 
        // más detalles, vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "IdBonos,Departamento,Bono,Motivo,Fecha,IdUsuarios")] tblBonos tblBonos)
        {
            if (ModelState.IsValid)
            {
                db.tblBonos.Add(tblBonos);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.IdUsuarios = new SelectList(db.tblUsuarios, "IdUsuarios", "Nombre", tblBonos.IdUsuarios);
            return View(tblBonos);
        }

        // GET: tblBonos/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tblBonos tblBonos = db.tblBonos.Find(id);
            if (tblBonos == null)
            {
                return HttpNotFound();
            }
            ViewBag.IdUsuarios = new SelectList(db.tblUsuarios, "IdUsuarios", "Nombre", tblBonos.IdUsuarios);
            return View(tblBonos);
        }

        // POST: tblBonos/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que quiere enlazarse. Para obtener 
        // más detalles, vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "IdBonos,Departamento,Bono,Motivo,Fecha,IdUsuarios")] tblBonos tblBonos)
        {
            if (ModelState.IsValid)
            {
                db.Entry(tblBonos).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.IdUsuarios = new SelectList(db.tblUsuarios, "IdUsuarios", "Nombre", tblBonos.IdUsuarios);
            return View(tblBonos);
        }

        // GET: tblBonos/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tblBonos tblBonos = db.tblBonos.Find(id);
            if (tblBonos == null)
            {
                return HttpNotFound();
            }
            return View(tblBonos);
        }

        // POST: tblBonos/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            tblBonos tblBonos = db.tblBonos.Find(id);
            db.tblBonos.Remove(tblBonos);
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
