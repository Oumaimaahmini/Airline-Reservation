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
    public class VolsController : Controller
    {
        private volDBEntities db = new volDBEntities();

        // GET: Vols
        public ActionResult Index()
        {
            return View(db.Vol.ToList());
        }
        public ActionResult Volii()
        {
            return View();
        }

        // GET: Vols/Details/5
        public ActionResult Details(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Vol vol = db.Vol.Find(id);
            if (vol == null)
            {
                return HttpNotFound();
            }
            return View(vol);
        }

        // GET: Vols/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Vols/Create
        // Afin de déjouer les attaques par survalidation, activez les propriétés spécifiques auxquelles vous voulez établir une liaison. Pour 
        // plus de détails, consultez https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "id,nombre_max,destination,ville_depart,date_depart,prix,compagnie")] Vol vol)
        {
            if (ModelState.IsValid)
            {
                db.Vol.Add(vol);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(vol);
        }

        // GET: Vols/Edit/5
        public ActionResult Edit(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Vol vol = db.Vol.Find(id);
            if (vol == null)
            {
                return HttpNotFound();
            }
            return View(vol);
        }

        // POST: Vols/Edit/5
        // Afin de déjouer les attaques par survalidation, activez les propriétés spécifiques auxquelles vous voulez établir une liaison. Pour 
        // plus de détails, consultez https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "id,nombre_max,destination,ville_depart,date_depart,prix,compagnie")] Vol vol)
        {
            if (ModelState.IsValid)
            {
                db.Entry(vol).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(vol);
        }

        // GET: Vols/Delete/5
        public ActionResult Delete(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Vol vol = db.Vol.Find(id);
            if (vol == null)
            {
                return HttpNotFound();
            }
            return View(vol);
        }

        // POST: Vols/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(string id)
        {
            Vol vol = db.Vol.Find(id);
            db.Vol.Remove(vol);
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
