using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using SZS;

namespace SZS.Controllers
{
    [Authorize(Roles = "Kierownik")]
    public class NazwaSzkoleniaController : Controller
    {
        private SZSModel db = new SZSModel();

        // GET: NazwaSzkolenia
        public ActionResult Index()
        {
            var nazwaSzkolenia = db.NazwaSzkolenia.Include(n => n.Kategoria);
            return View(nazwaSzkolenia.ToList());
        }

        // GET: NazwaSzkolenia/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            NazwaSzkolenia nazwaSzkolenia = db.NazwaSzkolenia.Find(id);
            if (nazwaSzkolenia == null)
            {
                return HttpNotFound();
            }
            return View(nazwaSzkolenia);
        }

        // GET: NazwaSzkolenia/Create
        public ActionResult Create()
        {
            ViewBag.IdKategorii = new SelectList(db.Kategoria, "Id", "Nazwa");
            return View();
        }

        // POST: NazwaSzkolenia/Create
        // Aby zapewnić ochronę przed atakami polegającymi na przesyłaniu dodatkowych danych, włącz określone właściwości, z którymi chcesz utworzyć powiązania.
        // Aby uzyskać więcej szczegółów, zobacz https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Nazwa,OpisSzkolenia,IdKategorii")] NazwaSzkolenia nazwaSzkolenia)
        {
            if (ModelState.IsValid)
            {
                db.NazwaSzkolenia.Add(nazwaSzkolenia);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.IdKategorii = new SelectList(db.Kategoria, "Id", "Nazwa", nazwaSzkolenia.IdKategorii);
            return View(nazwaSzkolenia);
        }

        // GET: NazwaSzkolenia/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            NazwaSzkolenia nazwaSzkolenia = db.NazwaSzkolenia.Find(id);
            if (nazwaSzkolenia == null)
            {
                return HttpNotFound();
            }
            ViewBag.IdKategorii = new SelectList(db.Kategoria, "Id", "Nazwa", nazwaSzkolenia.IdKategorii);
            return View(nazwaSzkolenia);
        }

        // POST: NazwaSzkolenia/Edit/5
        // Aby zapewnić ochronę przed atakami polegającymi na przesyłaniu dodatkowych danych, włącz określone właściwości, z którymi chcesz utworzyć powiązania.
        // Aby uzyskać więcej szczegółów, zobacz https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Nazwa,OpisSzkolenia,IdKategorii")] NazwaSzkolenia nazwaSzkolenia)
        {
            if (ModelState.IsValid)
            {
                db.Entry(nazwaSzkolenia).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.IdKategorii = new SelectList(db.Kategoria, "Id", "Nazwa", nazwaSzkolenia.IdKategorii);
            return View(nazwaSzkolenia);
        }

        // GET: NazwaSzkolenia/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            NazwaSzkolenia nazwaSzkolenia = db.NazwaSzkolenia.Find(id);
            if (nazwaSzkolenia == null)
            {
                return HttpNotFound();
            }
            return View(nazwaSzkolenia);
        }

        // POST: NazwaSzkolenia/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            NazwaSzkolenia nazwaSzkolenia = db.NazwaSzkolenia.Find(id);
            db.NazwaSzkolenia.Remove(nazwaSzkolenia);
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
