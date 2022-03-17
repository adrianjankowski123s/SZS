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
    public class WyksztalcenieController : Controller
    {
        private SZSModel db = new SZSModel();

        // GET: Wyksztalcenie
        public ActionResult Index()
        {
            return View(db.Wyksztalcenie.ToList());
        }

        // GET: Wyksztalcenie/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Wyksztalcenie wyksztalcenie = db.Wyksztalcenie.Find(id);
            if (wyksztalcenie == null)
            {
                return HttpNotFound();
            }
            return View(wyksztalcenie);
        }

        // GET: Wyksztalcenie/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Wyksztalcenie/Create
        // Aby zapewnić ochronę przed atakami polegającymi na przesyłaniu dodatkowych danych, włącz określone właściwości, z którymi chcesz utworzyć powiązania.
        // Aby uzyskać więcej szczegółów, zobacz https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Nazwa")] Wyksztalcenie wyksztalcenie)
        {
            if (ModelState.IsValid)
            {
                db.Wyksztalcenie.Add(wyksztalcenie);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(wyksztalcenie);
        }

        // GET: Wyksztalcenie/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Wyksztalcenie wyksztalcenie = db.Wyksztalcenie.Find(id);
            if (wyksztalcenie == null)
            {
                return HttpNotFound();
            }
            return View(wyksztalcenie);
        }

        // POST: Wyksztalcenie/Edit/5
        // Aby zapewnić ochronę przed atakami polegającymi na przesyłaniu dodatkowych danych, włącz określone właściwości, z którymi chcesz utworzyć powiązania.
        // Aby uzyskać więcej szczegółów, zobacz https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Nazwa")] Wyksztalcenie wyksztalcenie)
        {
            if (ModelState.IsValid)
            {
                db.Entry(wyksztalcenie).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(wyksztalcenie);
        }

        // GET: Wyksztalcenie/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Wyksztalcenie wyksztalcenie = db.Wyksztalcenie.Find(id);
            if (wyksztalcenie == null)
            {
                return HttpNotFound();
            }
            return View(wyksztalcenie);
        }

        // POST: Wyksztalcenie/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Wyksztalcenie wyksztalcenie = db.Wyksztalcenie.Find(id);
            db.Wyksztalcenie.Remove(wyksztalcenie);
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
