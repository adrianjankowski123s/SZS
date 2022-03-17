using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using SZS;

namespace SZS.Views
{
    [Authorize(Roles = "Kierownik")]
    public class UmiejetnosciController : Controller
    {
        private SZSModel db = new SZSModel();

        // GET: Umiejetnosci
        public ActionResult Index()
        {
            return View(db.Umiejetnosci.ToList());
        }

        // GET: Umiejetnosci/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Umiejetnosci umiejetnosci = db.Umiejetnosci.Find(id);
            if (umiejetnosci == null)
            {
                return HttpNotFound();
            }
            return View(umiejetnosci);
        }

        // GET: Umiejetnosci/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Umiejetnosci/Create
        // Aby zapewnić ochronę przed atakami polegającymi na przesyłaniu dodatkowych danych, włącz określone właściwości, z którymi chcesz utworzyć powiązania.
        // Aby uzyskać więcej szczegółów, zobacz https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Nazwa")] Umiejetnosci umiejetnosci)
        {
            if (ModelState.IsValid)
            {
                db.Umiejetnosci.Add(umiejetnosci);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(umiejetnosci);
        }

        // GET: Umiejetnosci/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Umiejetnosci umiejetnosci = db.Umiejetnosci.Find(id);
            if (umiejetnosci == null)
            {
                return HttpNotFound();
            }
            return View(umiejetnosci);
        }

        // POST: Umiejetnosci/Edit/5
        // Aby zapewnić ochronę przed atakami polegającymi na przesyłaniu dodatkowych danych, włącz określone właściwości, z którymi chcesz utworzyć powiązania.
        // Aby uzyskać więcej szczegółów, zobacz https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Nazwa")] Umiejetnosci umiejetnosci)
        {
            if (ModelState.IsValid)
            {
                db.Entry(umiejetnosci).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(umiejetnosci);
        }

        // GET: Umiejetnosci/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Umiejetnosci umiejetnosci = db.Umiejetnosci.Find(id);
            if (umiejetnosci == null)
            {
                return HttpNotFound();
            }
            return View(umiejetnosci);
        }

        // POST: Umiejetnosci/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Umiejetnosci umiejetnosci = db.Umiejetnosci.Find(id);
            db.Umiejetnosci.Remove(umiejetnosci);
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
