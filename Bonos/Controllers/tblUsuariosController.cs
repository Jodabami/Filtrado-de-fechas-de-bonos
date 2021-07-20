using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Bonos.Models;

namespace Bonos.Controllers
{
    public class tblUsuariosController : Controller
    {
        private BonosEntities db = new BonosEntities();

        // GET: tblUsuarios
        public ActionResult Index()
        {
            return View(db.tblUsuarios.ToList());
        }

        // GET: tblUsuarios/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tblUsuarios tblUsuarios = db.tblUsuarios.Find(id);
            if (tblUsuarios == null)
            {
                return HttpNotFound();
            }
            return View(tblUsuarios);
        }

        // GET: tblUsuarios/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: tblUsuarios/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que quiere enlazarse. Para obtener 
        // más detalles, vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "IdUsuarios,Nombre,Apellido")] tblUsuarios tblUsuarios)
        {
            if (ModelState.IsValid)
            {
                db.tblUsuarios.Add(tblUsuarios);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(tblUsuarios);
        }

        // GET: tblUsuarios/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tblUsuarios tblUsuarios = db.tblUsuarios.Find(id);
            if (tblUsuarios == null)
            {
                return HttpNotFound();
            }
            return View(tblUsuarios);
        }

        // POST: tblUsuarios/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que quiere enlazarse. Para obtener 
        // más detalles, vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "IdUsuarios,Nombre,Apellido")] tblUsuarios tblUsuarios)
        {
            if (ModelState.IsValid)
            {
                db.Entry(tblUsuarios).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(tblUsuarios);
        }

        // GET: tblUsuarios/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tblUsuarios tblUsuarios = db.tblUsuarios.Find(id);
            if (tblUsuarios == null)
            {
                return HttpNotFound();
            }
            return View(tblUsuarios);
        }

        // POST: tblUsuarios/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            tblUsuarios tblUsuarios = db.tblUsuarios.Find(id);
            db.tblUsuarios.Remove(tblUsuarios);
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
