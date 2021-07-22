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
    public class CustomersController : Controller
    {
        private ExamenContext db = new ExamenContext();

        /// <summary>
        /// Metodo principal que nos retorna una lista de Customer
        /// </summary>
        /// <returns></returns>
        // GET: Customers
        public ActionResult Index()
        {
            var customers = db.Customers.Include(c => c.Buildings);
            return View(customers.ToList());
        }

        /// <summary>
        /// Buscar un customer en la base de datos y nos retorna la información en una pantalla detalle
        /// </summary>
        /// <returns></returns>
        // GET: Customers/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Customers customers = db.Customers.Find(id);
            if (customers == null)
            {
                return HttpNotFound();
            }
            return View(customers);
        }

        /// <summary>
        /// Metodo que inicializa la pantalla para crear un customer y nos obtiene los building activos
        /// </summary>
        /// <returns></returns>
        // GET: Customers/Create
        public ActionResult Create()
        {
            ViewBag.FKBuilding = new SelectList(db.Buildings.Where(p => p.Available == true), "PKBuilding", "Building");
            return View();
        }

        // POST: Customers/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "PKCustomer,Customer,Prefix,FKBuilding,Available")] Customers customers)
        {
            if (ModelState.IsValid)
            {
                db.Customers.Add(customers);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.FKBuilding = new SelectList(db.Buildings.Where(p => p.Available == true), "PKBuilding", "Building", customers.FKBuilding);
            return View(customers);
        }

        /// <summary>
        /// Nos regresa un customer seleccionado para poder ser editado
        /// </summary>
        /// <returns></returns>
        // GET: Customers/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Customers customers = db.Customers.Find(id);
            if (customers == null)
            {
                return HttpNotFound();
            }
            ViewBag.FKBuilding = new SelectList(db.Buildings.Where(p => p.Available == true), "PKBuilding", "Building", customers.FKBuilding);
            return View(customers);
        }

        // POST: Customers/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "PKCustomer,Customer,Prefix,FKBuilding,Available")] Customers customers)
        {
            if (ModelState.IsValid)
            {
                db.Entry(customers).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.FKBuilding = new SelectList(db.Buildings.Where(p => p.Available == true), "PKBuilding", "Building", customers.FKBuilding);
            return View(customers);
        }

        /// <summary>
        /// Elimina el customer seleccionado
        /// </summary>
        /// <returns></returns>
        // GET: Customers/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Customers customers = db.Customers.Find(id);
            if (customers == null)
            {
                return HttpNotFound();
            }
            return View(customers);
        }

        // POST: Customers/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Customers customers = db.Customers.Find(id);
            db.Customers.Remove(customers);
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
