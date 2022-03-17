using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using SZS;
using SZS.Models;

namespace SZS.Controllers
{
    [Authorize(Roles = "Kierownik,Trener")]
    public class UczestnikController : Controller
    {
        private SZSModel db = new SZSModel();
        private Models.SZSModel context = new Models.SZSModel();
        // GET: Uczestnik
        public ActionResult Index()
        {

            var uczestnicy = db.Uczestnik.ToList();
            
            return View(uczestnicy);
        }

        public ActionResult SzkolenieUczestnicyList(int? id)
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

            var uczestnicy = db.Uczestnik.Where(x=>x.IdSzkolenia == szkolenie.Id).ToList();

            ViewBag.IdSzkole = szkolenie.Id;
            ViewBag.NazwaSzkole = szkolenie.NazwaSzkolenia.Nazwa;

            return View(uczestnicy);
        }


        [HttpGet]
        public JsonResult GetUsers()
        {
            var users = context.Users.ToList();
            
            return Json(users, JsonRequestBehavior.AllowGet);
        }



        // GET: Uczestnik/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Uczestnik uczestnik = db.Uczestnik.SingleOrDefault(m => m.Id == id); 
            if (uczestnik == null)
            {
                return HttpNotFound();
            }
            return View(uczestnik);
        }

        

         

        // GET: Uczestnik/Create/5
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

            var allusers = context.Users.ToList();
            var uczestnik = allusers.Where(x => x.Roles.Select(role => role.RoleId).Contains(3)).ToList();
            var users = new SelectList(context.Users, "Id", "UserName");
            var result = new List<SelectListItem>();
            foreach (var user in users)
            {
                foreach (var u in uczestnik)
                {
                    if(user.Value == u.Id.ToString())
                    {
                        result.Add(user);
                    }
                }
            }
            ViewBag.IdUczestnika = result;
            ViewBag.IdSzkolenia = szkolenie;
            ViewBag.IdSzkole = szkolenie.Id;
            TempData["IdSzkol"] = szkolenie.Id;

            ViewBag.NazwaSzkolenia = szkolenie.NazwaSzkolenia.Nazwa;
            return View();
        }

        // POST: Uczestnik/Create
        // Aby zapewnić ochronę przed atakami polegającymi na przesyłaniu dodatkowych danych, włącz określone właściwości, z którymi chcesz utworzyć powiązania.
        // Aby uzyskać więcej szczegółów, zobacz https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "IdUczestnika,IdSzkolenia")] Uczestnik uczestnik)
        {
            if (ModelState.IsValid)
            {
                
                    var id = TempData["IdSzkol"];
                    uczestnik.IdSzkolenia = (int)id;
                
                
                db.Uczestnik.Add(uczestnik);
                db.SaveChanges();
                return RedirectToAction("SzkolenieUczestnicyList", new { id = id });
            }

            ViewBag.IdUczestnika = new SelectList(context.Users, "Id", "UserName", uczestnik.IdUczestnika);
            ViewBag.IdSzkolenia = new SelectList(db.NazwaSzkolenia, "Id", "NazwaSzkolenia.Nazwa", uczestnik.IdSzkolenia);
            return View(uczestnik);
        }

        // GET: Uczestnik/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Uczestnik uczestnik = db.Uczestnik.SingleOrDefault(m => m.Id == id); 
            if (uczestnik == null)
            {
                return HttpNotFound();
            }
            TempData["IdSzkol"] = uczestnik.IdSzkolenia;
            ViewBag.IdUczestnika = new SelectList(db.AspNetUsers, "Id", "Email", uczestnik.IdUczestnika);
            ViewBag.IdSzkolenia = new SelectList(db.Szkolenie, "Id", "NazwaSzkolenia.Nazwa", uczestnik.IdSzkolenia);
            return View(uczestnik);
        }

        // POST: Uczestnik/Edit/5
        // Aby zapewnić ochronę przed atakami polegającymi na przesyłaniu dodatkowych danych, włącz określone właściwości, z którymi chcesz utworzyć powiązania.
        // Aby uzyskać więcej szczegółów, zobacz https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "IdUczestnika,IdSzkolenia")] Uczestnik uczestnik)
        {
            if (ModelState.IsValid)
            {
                db.Entry(uczestnik).State = EntityState.Modified;
                db.SaveChanges();
                int id2 = (int)TempData["IdSzkol"];

                return RedirectToAction("SzkolenieUczestnicyList", new { id = id2 });
            }
            ViewBag.IdUczestnika = new SelectList(db.AspNetUsers, "Id", "Email", uczestnik.IdUczestnika);
            ViewBag.IdSzkolenia = new SelectList(db.Szkolenie, "Id", "NazwaSzkolenia.Nazwa", uczestnik.IdSzkolenia);
            return View(uczestnik);
        }

        // GET: Uczestnik/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Uczestnik uczestnik = db.Uczestnik.SingleOrDefault(m=>m.Id == id);
            if (uczestnik == null)
            {
                return HttpNotFound();
            }
            TempData["IdSzkol"] = uczestnik.IdSzkolenia; 
            
            return View(uczestnik);
        }

        // POST: Uczestnik/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Uczestnik uczestnik = db.Uczestnik.SingleOrDefault(m => m.Id == id);
            db.Uczestnik.Remove(uczestnik);
            db.SaveChanges();
            int id2 = (int)TempData["IdSzkol"];

            return RedirectToAction("SzkolenieUczestnicyList", new { id = id2 });
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
