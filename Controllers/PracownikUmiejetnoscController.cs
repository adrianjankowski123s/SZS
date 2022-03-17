using Microsoft.AspNet.Identity.Owin;
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

    public class PracownikUmiejetnoscController : Controller
    {
        private SZSModel db = new SZSModel();
        private Models.SZSModel context = new Models.SZSModel();

        private ApplicationUserManager _userManager;
        public ApplicationUserManager UserManager
        {
            get
            {
                return _userManager ?? HttpContext.GetOwinContext().GetUserManager<ApplicationUserManager>();
            }
            private set
            {
                _userManager = value;
            }
        }

        [Authorize(Roles = "Kierownik, Trener")]
        // GET: PracownikUmiejetnosc
        public ActionResult Index(string email)
        {

            if (email == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var user = UserManager.Users.Where(x => x.Email == email).FirstOrDefault();

            
            ViewBag.NazwaUzytkownika = user.Imie + " " + user.Nazwisko;
            ViewBag.IdUzytkownik = user.Id;
            
            var pracownikUmiejetnosc = db.PracownikUmiejetnosc.Where(x => x.IdUzytkownika == user.Id);
            return View(pracownikUmiejetnosc.ToList());
        }

        [Authorize(Roles = "Kierownik")]
        // GET: PracownikUmiejetnosc/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PracownikUmiejetnosc pracownikUmiejetnosc = db.PracownikUmiejetnosc.Find(id);
            if (pracownikUmiejetnosc == null)
            {
                return HttpNotFound();
            }
            return View(pracownikUmiejetnosc);
        }
        [Authorize(Roles = "Kierownik")]
        // GET: PracownikUmiejetnosc/Create
        public ActionResult Create(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            

            var allusers = context.Users.ToList();

            var uczestnik = allusers.Where(x => x.Roles.Select(role => role.RoleId).Contains(2)).ToList();
            var users = new SelectList(context.Users, "Id", "UserName");
            var result = new List<SelectListItem>();
            foreach (var user in users)
            {
                foreach (var u in uczestnik)
                {
                    if (user.Value == u.Id.ToString())
                    {
                        result.Add(user);
                    }
                }
            }
            var us = UserManager.Users.Where(x => x.Id == id).FirstOrDefault();

            TempData["IdUzytkownik"] = us.Id;

            TempData["Email"] = us.Email;
            ViewBag.Email = us.Email;
            ViewBag.IdUzytkownika = result;
            ViewBag.IdUmiejetnosc = new SelectList(db.Umiejetnosci, "Id", "Nazwa");
            return View();
        }

        // POST: PracownikUmiejetnosc/Create
        // Aby zapewnić ochronę przed atakami polegającymi na przesyłaniu dodatkowych danych, włącz określone właściwości, z którymi chcesz utworzyć powiązania.
        // Aby uzyskać więcej szczegółów, zobacz https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Kierownik")]
        public ActionResult Create([Bind(Include = "IdUzytkownika,IdUmiejetnosc")] PracownikUmiejetnosc pracownikUmiejetnosc)
        {
            if (ModelState.IsValid)
            {
                var id = TempData["IdUzytkownik"];
                var email = TempData["Email"];
                pracownikUmiejetnosc.IdUzytkownika = (int)id;

                db.PracownikUmiejetnosc.Add(pracownikUmiejetnosc);
                db.SaveChanges();
                return RedirectToAction("Index", new { email = email });
            }

            ViewBag.IdUzytkownika = new SelectList(db.AspNetUsers, "Id", "Email", pracownikUmiejetnosc.IdUzytkownika);
            ViewBag.IdUmiejetnosc = new SelectList(db.Umiejetnosci, "Id", "Nazwa", pracownikUmiejetnosc.IdUmiejetnosc);
            return View(pracownikUmiejetnosc);
        }

        [Authorize(Roles = "Kierownik")]
        // GET: PracownikUmiejetnosc/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PracownikUmiejetnosc pracownikUmiejetnosc = db.PracownikUmiejetnosc.Find(id);
            if (pracownikUmiejetnosc == null)
            {
                return HttpNotFound();
            }
            ViewBag.IdUzytkownika = new SelectList(db.AspNetUsers, "Id", "Email", pracownikUmiejetnosc.IdUzytkownika);
            ViewBag.IdUmiejetnosc = new SelectList(db.Umiejetnosci, "Id", "Nazwa", pracownikUmiejetnosc.IdUmiejetnosc);
            return View(pracownikUmiejetnosc);
        }

        // POST: PracownikUmiejetnosc/Edit/5
        // Aby zapewnić ochronę przed atakami polegającymi na przesyłaniu dodatkowych danych, włącz określone właściwości, z którymi chcesz utworzyć powiązania.
        // Aby uzyskać więcej szczegółów, zobacz https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Kierownik")]
        public ActionResult Edit([Bind(Include = "IdUzytkownika,IdUmiejetnosc")] PracownikUmiejetnosc pracownikUmiejetnosc)
        {
            if (ModelState.IsValid)
            {
                db.Entry(pracownikUmiejetnosc).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.IdUzytkownika = new SelectList(db.AspNetUsers, "Id", "Email", pracownikUmiejetnosc.IdUzytkownika);
            ViewBag.IdUmiejetnosc = new SelectList(db.Umiejetnosci, "Id", "Nazwa", pracownikUmiejetnosc.IdUmiejetnosc);
            return View(pracownikUmiejetnosc);
        }

        // GET: PracownikUmiejetnosc/Delete/5
        [Authorize(Roles = "Kierownik")]
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PracownikUmiejetnosc pracownikUmiejetnosc = db.PracownikUmiejetnosc.SingleOrDefault(m=>m.Id == id);
            if (pracownikUmiejetnosc == null)
            {
                return HttpNotFound();
            }
            var user = UserManager.Users.Where(x => x.Id == pracownikUmiejetnosc.IdUzytkownika).FirstOrDefault();
            TempData["EmailUser"] = user.Email;
            ViewBag.Email = user.Email;


            return View(pracownikUmiejetnosc);
        }

        // POST: PracownikUmiejetnosc/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Kierownik")]
        public ActionResult DeleteConfirmed(int id)
        {
            PracownikUmiejetnosc pracownikUmiejetnosc = db.PracownikUmiejetnosc.SingleOrDefault(m => m.Id == id);
            db.PracownikUmiejetnosc.Remove(pracownikUmiejetnosc);
            db.SaveChanges();
            string email = (string)TempData["EmailUser"];

            return RedirectToAction("Index", new { email = email });
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
