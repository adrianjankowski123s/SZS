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
    public class TerminarzController : Controller
    {
        private SZSModel db = new SZSModel();

        // GET: Terminarz
        public ActionResult Index(int? id)
        {
            
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Szkolenie szkolenie = db.Szkolenie.Find(id);
            if (szkolenie == null)
            {
                return HttpNotFound();
            }
            
            var terminarz = db.Terminarz.Where(x => x.IdSzkolenia == szkolenie.Id).Include(t => t.Szkolenie).Include(t => t.Temat);

            ViewBag.NazwaSzkole = szkolenie.NazwaSzkolenia.Nazwa;
            ViewBag.IdSzkole = szkolenie.Id;
            return View(terminarz.ToList());
        }
        [Authorize(Roles = "Kierownik,Trener")]
        // GET: Terminarz/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Terminarz terminarz = db.Terminarz.Find(id);
            if (terminarz == null)
            {
                return HttpNotFound();
            }
            return View(terminarz);
        }
        [Authorize(Roles = "Kierownik,Trener")]
        // GET: Terminarz/Create
        public ActionResult Create(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Szkolenie szkolenie = db.Szkolenie.Find(id);
            if (szkolenie == null)
            {
                return HttpNotFound();
            }

            TempData["IdSzkol"] = szkolenie.Id;
            ViewBag.IdSzkole = szkolenie.Id;
            ViewBag.IdSzkolenia = new SelectList(db.Szkolenie, "Id", "NazwaSzkolenia.Nazwa");
            ViewBag.IdTematu = new SelectList(db.Temat, "Id", "Nazwa");
            return View();
        }
        [Authorize(Roles = "Kierownik,Trener")]
        // POST: Terminarz/Create
        // Aby zapewnić ochronę przed atakami polegającymi na przesyłaniu dodatkowych danych, włącz określone właściwości, z którymi chcesz utworzyć powiązania.
        // Aby uzyskać więcej szczegółów, zobacz https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,IdSzkolenia,IdTematu,LiczbaGodzin,TerminOd,TerminDo")] Terminarz terminarz)
        {

            if (ModelState.IsValid)
            {
                var id = TempData["IdSzkol"];
                terminarz.IdSzkolenia = (int)id;

                db.Terminarz.Add(terminarz);
                db.SaveChanges();
                return RedirectToAction("Index", new { id = id });
            }

            ViewBag.IdSzkolenia = new SelectList(db.Szkolenie, "Id", "NazwaSzkolenia.Nazwa", terminarz.IdSzkolenia);
            ViewBag.IdTematu = new SelectList(db.Temat, "Id", "Nazwa", terminarz.IdTematu);
            return View(terminarz);
        }

        // GET: Terminarz/Edit/5
        [Authorize(Roles = "Kierownik,Trener")]
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Terminarz terminarz = db.Terminarz.Find(id);
            if (terminarz == null)
            {
                return HttpNotFound();
            }
            TempData["IdSzkol"] = terminarz.IdSzkolenia;
            Szkolenie szkolenie = db.Szkolenie.Find(terminarz.IdSzkolenia);

            ViewBag.NazwaSzkole = szkolenie.NazwaSzkolenia.Nazwa;
            ViewBag.IdSzkolenia = new SelectList(db.Szkolenie, "Id", "NazwaSzkolenia.Nazwa", terminarz.IdSzkolenia);
            ViewBag.IdTematu = new SelectList(db.Temat, "Id", "Nazwa", terminarz.IdTematu);
            return View(terminarz);
        }

        // POST: Terminarz/Edit/5
        // Aby zapewnić ochronę przed atakami polegającymi na przesyłaniu dodatkowych danych, włącz określone właściwości, z którymi chcesz utworzyć powiązania.
        // Aby uzyskać więcej szczegółów, zobacz https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Kierownik,Trener")]
        public ActionResult Edit([Bind(Include = "Id,IdSzkolenia,IdTematu,LiczbaGodzin,TerminOd,TerminDo")] Terminarz terminarz)
        {
            if (ModelState.IsValid)
            {
                db.Entry(terminarz).State = EntityState.Modified;
                db.SaveChanges();

                int id2 = (int)TempData["IdSzkol"];
                return RedirectToAction("Index", new { id = id2 });
            }
            Szkolenie szkolenie = db.Szkolenie.Find(terminarz.IdSzkolenia);
            ViewBag.NazwaSzkole = szkolenie.NazwaSzkolenia.Nazwa;
            ViewBag.IdSzkolenia = new SelectList(db.Szkolenie, "Id", "NazwaSzkolenia.Nazwa", terminarz.IdSzkolenia);
            ViewBag.IdTematu = new SelectList(db.Temat, "Id", "Nazwa", terminarz.IdTematu);
            return View(terminarz);
        }

        // GET: Terminarz/Delete/5
        [Authorize(Roles = "Kierownik,Trener")]
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Terminarz terminarz = db.Terminarz.Find(id);
            if (terminarz == null)
            {
                return HttpNotFound();
            }
            TempData["IdSzkol"] = terminarz.IdSzkolenia;


            return View(terminarz);
        }

        // POST: Terminarz/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Kierownik,Trener")]
        public ActionResult DeleteConfirmed(int id)
        {
            Terminarz terminarz = db.Terminarz.Find(id);
            db.Terminarz.Remove(terminarz);
            db.SaveChanges();
            int id2 = (int)TempData["IdSzkol"];

            return RedirectToAction("Index", new { id = id2 });

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
