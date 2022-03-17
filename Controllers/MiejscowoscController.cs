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
    public class MiejscowoscController : Controller
    {
        private SZSModel db = new SZSModel();

        // GET: Miejscowosc
        public ActionResult Index()
        {

            var miejscowosc = db.Miejscowosc.Include(m => m.Wojewodztwo);
            return View(miejscowosc.ToList());
        }

        // GET: Miejscowosc/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Miejscowosc miejscowosc = db.Miejscowosc.Find(id);
            if (miejscowosc == null)
            {
                return HttpNotFound();
            }
            return View(miejscowosc);
        }

        // GET: Miejscowosc/Create
        public ActionResult Create()
        {
            ViewBag.IdWojewodztwa = new SelectList(db.Wojewodztwo, "Id", "Nazwa");
            return View();
        }

        // POST: Miejscowosc/Create
        // Aby zapewnić ochronę przed atakami polegającymi na przesyłaniu dodatkowych danych, włącz określone właściwości, z którymi chcesz utworzyć powiązania.
        // Aby uzyskać więcej szczegółów, zobacz https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Nazwa,IdWojewodztwa")] Miejscowosc miejscowosc)
        {
            if (ModelState.IsValid)
            {
                db.Miejscowosc.Add(miejscowosc);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.IdWojewodztwa = new SelectList(db.Wojewodztwo, "Id", "Nazwa", miejscowosc.IdWojewodztwa);
            return View(miejscowosc);
        }

        // GET: Miejscowosc/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Miejscowosc miejscowosc = db.Miejscowosc.Find(id);
            if (miejscowosc == null)
            {
                return HttpNotFound();
            }
            ViewBag.IdWojewodztwa = new SelectList(db.Wojewodztwo, "Id", "Nazwa", miejscowosc.IdWojewodztwa);
            return View(miejscowosc);
        }

        // POST: Miejscowosc/Edit/5
        // Aby zapewnić ochronę przed atakami polegającymi na przesyłaniu dodatkowych danych, włącz określone właściwości, z którymi chcesz utworzyć powiązania.
        // Aby uzyskać więcej szczegółów, zobacz https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Nazwa,IdWojewodztwa")] Miejscowosc miejscowosc)
        {
            if (ModelState.IsValid)
            {
                db.Entry(miejscowosc).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.IdWojewodztwa = new SelectList(db.Wojewodztwo, "Id", "Nazwa", miejscowosc.IdWojewodztwa);
            return View(miejscowosc);
        }

        // GET: Miejscowosc/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Miejscowosc miejscowosc = db.Miejscowosc.Find(id);
            if (miejscowosc == null)
            {
                return HttpNotFound();
            }
            return View(miejscowosc);
        }

        // POST: Miejscowosc/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Miejscowosc miejscowosc = db.Miejscowosc.Find(id);
            db.Miejscowosc.Remove(miejscowosc);
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
