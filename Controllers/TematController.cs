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
    [Authorize(Roles = "Kierownik,Trener")]
    public class TematController : Controller
    {
        private SZSModel db = new SZSModel();

        // GET: Temat
        public ActionResult Index()
        {
            return View(db.Temat.ToList());
        }

        // GET: Temat/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Temat temat = db.Temat.Find(id);
            if (temat == null)
            {
                return HttpNotFound();
            }
            return View(temat);
        }

        // GET: Temat/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Temat/Create
        // Aby zapewnić ochronę przed atakami polegającymi na przesyłaniu dodatkowych danych, włącz określone właściwości, z którymi chcesz utworzyć powiązania.
        // Aby uzyskać więcej szczegółów, zobacz https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Nazwa")] Temat temat)
        {
            if (ModelState.IsValid)
            {
                db.Temat.Add(temat);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(temat);
        }

        // GET: Temat/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Temat temat = db.Temat.Find(id);
            if (temat == null)
            {
                return HttpNotFound();
            }
            return View(temat);
        }

        // POST: Temat/Edit/5
        // Aby zapewnić ochronę przed atakami polegającymi na przesyłaniu dodatkowych danych, włącz określone właściwości, z którymi chcesz utworzyć powiązania.
        // Aby uzyskać więcej szczegółów, zobacz https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Nazwa")] Temat temat)
        {
            if (ModelState.IsValid)
            {
                db.Entry(temat).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(temat);
        }

        // GET: Temat/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Temat temat = db.Temat.Find(id);
            if (temat == null)
            {
                return HttpNotFound();
            }
            return View(temat);
        }

        // POST: Temat/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Temat temat = db.Temat.Find(id);
            db.Temat.Remove(temat);
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
