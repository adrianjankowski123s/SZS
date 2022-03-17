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
    public class FinansowanieController : Controller
    {
        private SZSModel db = new SZSModel();

        // GET: Finansowanie
        public ActionResult Index()
        {
            return View(db.Finansowanie.ToList());
        }

        // GET: Finansowanie/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Finansowanie finansowanie = db.Finansowanie.Find(id);
            if (finansowanie == null)
            {
                return HttpNotFound();
            }
            return View(finansowanie);
        }

        // GET: Finansowanie/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Finansowanie/Create
        // Aby zapewnić ochronę przed atakami polegającymi na przesyłaniu dodatkowych danych, włącz określone właściwości, z którymi chcesz utworzyć powiązania.
        // Aby uzyskać więcej szczegółów, zobacz https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Nazwa")] Finansowanie finansowanie)
        {
            if (ModelState.IsValid)
            {
                db.Finansowanie.Add(finansowanie);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(finansowanie);
        }

        // GET: Finansowanie/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Finansowanie finansowanie = db.Finansowanie.Find(id);
            if (finansowanie == null)
            {
                return HttpNotFound();
            }
            return View(finansowanie);
        }

        // POST: Finansowanie/Edit/5
        // Aby zapewnić ochronę przed atakami polegającymi na przesyłaniu dodatkowych danych, włącz określone właściwości, z którymi chcesz utworzyć powiązania.
        // Aby uzyskać więcej szczegółów, zobacz https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Nazwa")] Finansowanie finansowanie)
        {
            if (ModelState.IsValid)
            {
                db.Entry(finansowanie).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(finansowanie);
        }

        // GET: Finansowanie/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Finansowanie finansowanie = db.Finansowanie.Find(id);
            if (finansowanie == null)
            {
                return HttpNotFound();
            }
            return View(finansowanie);
        }

        // POST: Finansowanie/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Finansowanie finansowanie = db.Finansowanie.Find(id);
            db.Finansowanie.Remove(finansowanie);
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
