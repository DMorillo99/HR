﻿using System;
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
    public class countriesController : Controller
    {
        private HREntities db = new HREntities();

        // GET: countries
        public ActionResult Index()
        {
            var countries = db.countries.Include(c => c.region);
            return View(countries.ToList());
        }

        // GET: countries/Details/5
        public ActionResult Details(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            country country = db.countries.Find(id);
            if (country == null)
            {
                return HttpNotFound();
            }
            return View(country);
        }

        // GET: countries/Create
        public ActionResult Create()
        {
            ViewBag.region_id = new SelectList(db.regions, "region_id", "region_name");
            return View();
        }

        // POST: countries/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "country_id,country_name,region_id")] country country)
        {
            if (ModelState.IsValid)
            {
                db.countries.Add(country);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.region_id = new SelectList(db.regions, "region_id", "region_name", country.region_id);
            return View(country);
        }

        // GET: countries/Edit/5
        public ActionResult Edit(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            country country = db.countries.Find(id);
            if (country == null)
            {
                return HttpNotFound();
            }
            ViewBag.region_id = new SelectList(db.regions, "region_id", "region_name", country.region_id);
            return View(country);
        }

        // POST: countries/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "country_id,country_name,region_id")] country country)
        {
            if (ModelState.IsValid)
            {
                db.Entry(country).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.region_id = new SelectList(db.regions, "region_id", "region_name", country.region_id);
            return View(country);
        }

        // GET: countries/Delete/5
        public ActionResult Delete(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            country country = db.countries.Find(id);
            if (country == null)
            {
                return HttpNotFound();
            }
            return View(country);
        }

        // POST: countries/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(string id)
        {
            country country = db.countries.Find(id);
            db.countries.Remove(country);
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
