using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using HRWeb.Models;

namespace HRWeb.Controllers
{
    public class dependentsController : Controller
    {
        private HREntities db = new HREntities();

        // GET: dependents
        public ActionResult Index()
        {
            var dependents = db.dependents.Include(d => d.employee);
            return View(dependents.ToList());
        }

        // GET: dependents/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            dependent dependent = db.dependents.Find(id);
            if (dependent == null)
            {
                return HttpNotFound();
            }
            return View(dependent);
        }

        // GET: dependents/Create
        public ActionResult Create()
        {
            ViewBag.employee_id = new SelectList(db.employees, "employee_id", "first_name");
            return View();
        }

        // POST: dependents/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "dependent_id,first_name,last_name,relationship,employee_id")] dependent dependent)
        {
            if (ModelState.IsValid)
            {
                db.dependents.Add(dependent);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.employee_id = new SelectList(db.employees, "employee_id", "first_name", dependent.employee_id);
            return View(dependent);
        }

        // GET: dependents/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            dependent dependent = db.dependents.Find(id);
            if (dependent == null)
            {
                return HttpNotFound();
            }
            ViewBag.employee_id = new SelectList(db.employees, "employee_id", "first_name", dependent.employee_id);
            return View(dependent);
        }

        // POST: dependents/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "dependent_id,first_name,last_name,relationship,employee_id")] dependent dependent)
        {
            if (ModelState.IsValid)
            {
                db.Entry(dependent).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.employee_id = new SelectList(db.employees, "employee_id", "first_name", dependent.employee_id);
            return View(dependent);
        }

        // GET: dependents/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            dependent dependent = db.dependents.Find(id);
            if (dependent == null)
            {
                return HttpNotFound();
            }
            return View(dependent);
        }

        // POST: dependents/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            dependent dependent = db.dependents.Find(id);
            db.dependents.Remove(dependent);
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
