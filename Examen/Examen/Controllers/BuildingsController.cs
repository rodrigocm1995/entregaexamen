using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Examen.Data;
using Examen.Models;

namespace Examen.Controllers
{
    public class BuildingsController : Controller
    {
        private ExamenContext db = new ExamenContext();

        /// <summary>
        /// Metodo principal que nos retorna una lista de Building
        /// </summary>
        /// <returns></returns>
        // GET: Buildings
        public ActionResult Index()
        {
            return View(db.Buildings.ToList());
        }

        /// <summary>
        /// Nos regresa el building seleccionado para mostrarlo en una ventana de detalle
        /// </summary>
        /// <returns></returns>
        // GET: Buildings/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Buildings buildings = db.Buildings.Find(id);
            if (buildings == null)
            {
                return HttpNotFound();
            }
            return View(buildings);
        }

        /// <summary>
        /// Metodo principal para la pantalla de creación
        /// </summary>
        /// <returns></returns>

        // GET: Buildings/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Buildings/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "PKBuilding,Building,Available")] Buildings buildings)
        {
            if (ModelState.IsValid)
            {
                db.Buildings.Add(buildings);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(buildings);
        }

        /// <summary>
        /// Nos obtiene un building específico y lo regresa a nuestra pantalla de editar
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        // GET: Buildings/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Buildings buildings = db.Buildings.Find(id);
            if (buildings == null)
            {
                return HttpNotFound();
            }
            return View(buildings);
        }

        // POST: Buildings/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "PKBuilding,Building,Available")] Buildings buildings)
        {
            if (ModelState.IsValid)
            {
                db.Entry(buildings).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(buildings);
        }

        /// <summary>
        /// Nos permite eliminar un building
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        // GET: Buildings/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Buildings buildings = db.Buildings.Find(id);
            if (buildings == null)
            {
                return HttpNotFound();
            }
            return View(buildings);
        }

        // POST: Buildings/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Buildings buildings = db.Buildings.Find(id);
            db.Buildings.Remove(buildings);
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
