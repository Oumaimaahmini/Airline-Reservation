using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using SiteVol.Models;

namespace SiteVol.Controllers
{
    public class CompagniesController : Controller
    {
        private volDBEntities db = new volDBEntities();

        // GET: Compagnies
        public ActionResult Index()
        {
            return View(db.Compagnie.ToList());
        }

        // GET: Compagnies/Details/5
        public ActionResult Details(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Compagnie compagnie = db.Compagnie.Find(id);
            if (compagnie == null)
            {
                return HttpNotFound();
            }
            return View(compagnie);
        }

        // GET: Compagnies/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Compagnies/Create
        // Afin de déjouer les attaques par survalidation, activez les propriétés spécifiques auxquelles vous voulez établir une liaison. Pour 
        // plus de détails, consultez https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,nom,secteur,username,password")] Compagnie compagnie)
        {
            if (ModelState.IsValid)
            {
                db.Compagnie.Add(compagnie);
                db.SaveChanges();
                
            }

            return View(compagnie);
        }

        // GET: Compagnies/Edit/5
        public ActionResult Edit(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Compagnie compagnie = db.Compagnie.Find(id);
            if (compagnie == null)
            {
                return HttpNotFound();
            }
            return View(compagnie);
        }

        // POST: Compagnies/Edit/5
        // Afin de déjouer les attaques par survalidation, activez les propriétés spécifiques auxquelles vous voulez établir une liaison. Pour 
        // plus de détails, consultez https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,nom,secteur")] Compagnie compagnie)
        {
            if (ModelState.IsValid)
            {
                db.Entry(compagnie).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(compagnie);
        }

        // GET: Compagnies/Delete/5
        public ActionResult Delete(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Compagnie compagnie = db.Compagnie.Find(id);
            if (compagnie == null)
            {
                return HttpNotFound();
            }
            return View(compagnie);
        }

        // POST: Compagnies/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(string id)
        {
            Compagnie compagnie = db.Compagnie.Find(id);
            db.Compagnie.Remove(compagnie);
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
