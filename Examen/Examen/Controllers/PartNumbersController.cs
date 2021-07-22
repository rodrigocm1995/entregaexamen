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
    public class PartNumbersController : Controller
    {
        private ExamenContext db = new ExamenContext();

        /// <summary>
        /// Método principal que retorna la lista de PartNumbers
        /// </summary>
        /// <returns></returns>
        // GET: PartNumbers
        public ActionResult Index()
        {
            var partNumbers = db.PartNumbers.Include(p => p.Customers);
            return View(partNumbers.ToList());
        }

        /// <summary>
        /// Nos busca un partnumber específico y nos llena la pantalla de detalle
        /// </summary>
        /// <returns></returns>
        // GET: PartNumbers/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PartNumbers partNumbers = db.PartNumbers.Find(id);
            if (partNumbers == null)
            {
                return HttpNotFound();
            }
            return View(partNumbers);
        }

        /// <summary>
        /// Inicializa la pantalla de crear partnumber y nos obtiene los customer activos
        /// </summary>
        /// <returns></returns>
        // GET: PartNumbers/Create
        public ActionResult Create()
        {
            ViewBag.FKCustomer = new SelectList(db.Customers.Where(p => p.Available == true), "PKCustomer", "Customer");
            return View();
        }

        // POST: PartNumbers/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "PKPartNumber,PartNumber,FKCustomer,Available")] PartNumbers partNumbers)
        {
            if (ModelState.IsValid)
            {
                db.PartNumbers.Add(partNumbers);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.FKCustomer = new SelectList(db.Customers.Where(p => p.Available == true), "PKCustomer", "Customer", partNumbers.FKCustomer);
            return View(partNumbers);
        }

        /// <summary>
        /// Nos busca la información de un partnumber seleccionado para poder ser editado
        /// </summary>
        /// <returns></returns>
        // GET: PartNumbers/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PartNumbers partNumbers = db.PartNumbers.Find(id);
            if (partNumbers == null)
            {
                return HttpNotFound();
            }
            ViewBag.FKCustomer = new SelectList(db.Customers.Where(p => p.Available == true), "PKCustomer", "Customer", partNumbers.FKCustomer);
            return View(partNumbers);
        }

        // POST: PartNumbers/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "PKPartNumber,PartNumber,FKCustomer,Available")] PartNumbers partNumbers)
        {
            if (ModelState.IsValid)
            {
                db.Entry(partNumbers).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.FKCustomer = new SelectList(db.Customers.Where(p => p.Available == true), "PKCustomer", "Customer", partNumbers.FKCustomer);
            return View(partNumbers);
        }

        /// <summary>
        /// Elimina un partnumber
        /// </summary>
        /// <returns></returns>
        // GET: PartNumbers/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PartNumbers partNumbers = db.PartNumbers.Find(id);
            if (partNumbers == null)
            {
                return HttpNotFound();
            }
            return View(partNumbers);
        }

        // POST: PartNumbers/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            PartNumbers partNumbers = db.PartNumbers.Find(id);
            db.PartNumbers.Remove(partNumbers);
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
