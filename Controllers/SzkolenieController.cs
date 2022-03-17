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
using SZS.Extensions;
namespace SZS.Views
{
    public class SzkolenieController : Controller
    {
        private SZSModel db = new SZSModel();
        private Models.SZSModel context = new Models.SZSModel();
        // GET: Szkolenie

        public ActionResult Index(string sortOrder, string searchString)
        {
            ViewBag.NameSortParm = String.IsNullOrEmpty(sortOrder) ? "name_desc" : "";
            ViewBag.NameSortParm1 = String.IsNullOrEmpty(sortOrder) ? "name_desc1" : "";
            ViewBag.NameSortParm2 = String.IsNullOrEmpty(sortOrder) ? "name_desc2" : "";
            ViewBag.NameSortParm3 = String.IsNullOrEmpty(sortOrder) ? "name_desc3" : "";
            ViewBag.NameSortParm4 = String.IsNullOrEmpty(sortOrder) ? "name_desc4" : "";
            
            ViewBag.DateSortParm = sortOrder == "Date" ? "date_desc" : "Date";
            ViewBag.DateSortParm1 = sortOrder == "Date" ? "date_desc" : "Date1";

            var szkolenie = db.Szkolenie.Include(s => s.AspNetUsers).Include
      (s => s.AspNetUsers1).Include(s => s.Finansowanie).Include
      (s => s.Kategoria).Include(s => s.Miejscowosc).Include(s => s.Wojewodztwo).Include(s => s.NazwaSzkolenia);

            var model = new List<Szkolenie>();

            if (User.Identity.IsAuthenticated && User.IsInRole("Kierownik") || User.IsInRole("Trener"))
            {
                szkolenie = db.Szkolenie.Include(s => s.AspNetUsers).Include
                    (s => s.AspNetUsers1).Include(s => s.Finansowanie).Include
                    (s => s.Kategoria).Include(s => s.Miejscowosc).Include(s => s.Wojewodztwo).Include(s => s.NazwaSzkolenia);

                foreach (var item in szkolenie)
                {
                    model.Add(item);
                }
            }

            else
            {
                foreach (var item in szkolenie)
                {
                    if (item.CzyWidoczne == true)
                    {
                        model.Add(item);
                    }
                }
            }


            var m = model.AsQueryable();


            if (!String.IsNullOrEmpty(searchString))
            {
                m = m.Where(s => s.NazwaSzkolenia.Nazwa.Contains(searchString)
                                       || s.Kategoria.Nazwa.Contains(searchString)
                                       || s.Miejscowosc.Nazwa.Contains(searchString)
                                       || s.Ulica.Contains(searchString)
                                       || s.DataOd.ToString().Contains(searchString)
                                       || s.DataDo.ToString().Contains(searchString)
                                       || s.Koszt.ToString().Contains(searchString)
                                       || s.Kategoria.Nazwa.ToLower().Contains(searchString)
                                       || s.Miejscowosc.Nazwa.ToLower().Contains(searchString)
                                       || s.Ulica.ToLower().Contains(searchString)
                                       || s.NazwaSzkolenia.Nazwa.ToLower().ToString().Contains(searchString)
                                       );
            }

            switch (sortOrder)
            {
                case "name_desc":
                    m = m.OrderByDescending(s => s.NazwaSzkolenia.Nazwa);
                    break;
                case "name_desc1":
                    m = m.OrderBy(s => s.Kategoria.Nazwa);
                    break;
                case "name_desc2":
                    m = m.OrderBy(s => s.Miejscowosc.Nazwa);
                    break;
                case "name_desc3":
                    m = m.OrderBy(s => s.Ulica);
                    break;
                case "Date":
                    m = m.OrderBy(s => s.DataOd);
                    break;
                case "Date1":
                    m = m.OrderBy(s => s.DataDo);
                    break;
                case "name_desc4":
                    m = m.OrderBy(s => s.Koszt);
                    break;
                
                default:
                    m = m.OrderBy(s => s.NazwaSzkolenia.Nazwa);
                    break;
            }


            return View(m.ToList());
        }

        [Authorize(Roles = "Kierownik,Trener, Uczestnik")]
        public ActionResult MojeSzkolenia()
        {
            var uczestnicy = db.Uczestnik.ToList();
            int id = int.Parse(User.Identity.GetIdUczestnik());
            
            var szkolenie = db.Szkolenie.Include(s => s.AspNetUsers).Include(s => s.AspNetUsers1).Include
                (s => s.Finansowanie).Include(s => s.Kategoria).Include(s => s.Miejscowosc).Include
                (s => s.Wojewodztwo).Include(s => s.NazwaSzkolenia);

            var model = new List<Szkolenie>();

            foreach (var item in szkolenie)
            {
                if(item.Uczestnik.Any(x=>x.IdUczestnika == id))
                {
                    model.Add(item);
                }
                if (item.IdKierownika == id)
                {
                    model.Add(item);
                }
                if (item.IdTrenera == id)
                {
                    model.Add(item);
                }
            }
            return View(model.ToList());
        }

        [HttpGet]
        public JsonResult GetKierownik()
        {
            var users = context.Users.ToList();
         
            return Json(users, JsonRequestBehavior.AllowGet);
        }
       
        [HttpGet]
        public JsonResult GetTrener()
        {
            var users = context.Users.ToList();

            return Json(users, JsonRequestBehavior.AllowGet);
        }

        [Authorize(Roles = "Kierownik,Trener")]
        public ActionResult SzkolenieUsersList(int? id)
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
            
       
            return View();
        }
        [Authorize(Roles = "Kierownik,Trener")]
        // GET: Szkolenie/Details/5
        public ActionResult Details(int? id)
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
            return View(szkolenie);
        }
        [Authorize(Roles = "Kierownik")]
        // GET: Szkolenie/Create
        public ActionResult Create()
        {

            var allusers = context.Users.ToList();

            var noRolesUsers = allusers.Where(x => !x.Roles.Select(role => role.RoleId).Contains(1)).ToList();
            noRolesUsers = noRolesUsers.Where(x => !x.Roles.Select(role => role.RoleId).Contains(2)).ToList();
            noRolesUsers = noRolesUsers.Where(x => !x.Roles.Select(role => role.RoleId).Contains(3)).ToList();
            var noRolesUsersVM = noRolesUsers.Select(user => new UserViewModel { Email = user.Email }).ToList();

            var uzytkownicyVM = allusers.Select(user => new UserViewModel { Id = user.Id, Imie = user.Imie, Nazwisko = user.Nazwisko, Nazwa = user.Imie + " " + user.Nazwisko, Email = user.Email }).ToList();

            var uczestnik = allusers.Where(x => x.Roles.Select(role => role.RoleId).Contains(3)).ToList();
            var uczestnikVM = uczestnik.Select(user => new UserViewModel { Id = user.Id, Imie = user.Imie, Nazwisko = user.Nazwisko, Nazwa = user.Imie + " " + user.Nazwisko, Email = user.Email }).ToList();

            var trener = allusers.Where(x => x.Roles.Select(role => role.RoleId).Contains(2)).ToList();
            var trenerVM = trener.Select(user => new UserViewModel { Id = user.Id, Imie = user.Imie, Nazwisko = user.Nazwisko, Nazwa = user.Imie + " " + user.Nazwisko, Email = user.Email }).ToList();

            var kierownik = allusers.Where(x => x.Roles.Select(role => role.RoleId).Contains(1)).ToList();
            var kierownikVM = kierownik.Select(user => new UserViewModel { Id = user.Id, Imie = user.Imie, Nazwisko = user.Nazwisko, Nazwa = user.Imie + " " + user.Nazwisko, Email = user.Email }).ToList();



            var model = new UserViewModel { allUsers = uzytkownicyVM, Uczestnicy = uczestnikVM, Trenerzy = trenerVM, Kierownicy = kierownikVM, noRolesUsers = noRolesUsersVM };
            
            ViewBag.IdKierownika = new SelectList(model.Kierownicy, "Id", "Nazwa");
            ViewBag.IdTrenera = new SelectList(model.Trenerzy, "Id", "Nazwa");
            ViewBag.IdFinansowania = new SelectList(db.Finansowanie, "Id", "Nazwa");
            ViewBag.IdKategorii = new SelectList(db.Kategoria, "Id", "Nazwa");
            ViewBag.IdWojewodztwa = new SelectList(db.Wojewodztwo, "Id", "Nazwa");
            ViewBag.IdMiejscowosc = new SelectList(db.Miejscowosc, "Id", "Nazwa");
            ViewBag.IdNazwaSzkolenia = new SelectList(db.NazwaSzkolenia, "Id", "Nazwa");
            return View();
        }

        // POST: Szkolenie/Create
        // Aby zapewnić ochronę przed atakami polegającymi na przesyłaniu dodatkowych danych, włącz określone właściwości, z którymi chcesz utworzyć powiązania.
        // Aby uzyskać więcej szczegółów, zobacz https://go.microsoft.com/fwlink/?LinkId=317598.
        [Authorize(Roles = "Kierownik")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,IdNazwaSzkolenia,IdWojewodztwa,IdKategorii,IdKierownika,IdTrenera,LiczbaGodzin,DataOd,DataDo,KodPocztowy,IdMiejscowosc,Ulica,IdFinansowania,Koszt,CzyWydanoZaswiadczenie,CzyZrealizowano,KodSzkolenia,CzyOtwarte,CzyWidoczne,Uwagi")] Szkolenie szkolenie)
        {
            var allusers = context.Users.ToList();

            var noRolesUsers = allusers.Where(x => !x.Roles.Select(role => role.RoleId).Contains(1)).ToList();
            noRolesUsers = noRolesUsers.Where(x => !x.Roles.Select(role => role.RoleId).Contains(2)).ToList();
            noRolesUsers = noRolesUsers.Where(x => !x.Roles.Select(role => role.RoleId).Contains(3)).ToList();
            var noRolesUsersVM = noRolesUsers.Select(user => new UserViewModel { Email = user.Email }).ToList();

            var uzytkownicyVM = allusers.Select(user => new UserViewModel { Id = user.Id, Imie = user.Imie, Nazwisko = user.Nazwisko, Nazwa = user.Imie + " " + user.Nazwisko, Email = user.Email }).ToList();

            var uczestnik = allusers.Where(x => x.Roles.Select(role => role.RoleId).Contains(3)).ToList();
            var uczestnikVM = uczestnik.Select(user => new UserViewModel { Id = user.Id, Imie = user.Imie, Nazwisko = user.Nazwisko, Nazwa = user.Imie + " " + user.Nazwisko, Email = user.Email }).ToList();

            var trener = allusers.Where(x => x.Roles.Select(role => role.RoleId).Contains(2)).ToList();
            var trenerVM = trener.Select(user => new UserViewModel { Id = user.Id, Imie = user.Imie, Nazwisko = user.Nazwisko, Nazwa = user.Imie + " " + user.Nazwisko, Email = user.Email }).ToList();

            var kierownik = allusers.Where(x => x.Roles.Select(role => role.RoleId).Contains(1)).ToList();
            var kierownikVM = kierownik.Select(user => new UserViewModel { Id = user.Id, Imie = user.Imie, Nazwisko = user.Nazwisko, Nazwa = user.Imie + " " + user.Nazwisko, Email = user.Email }).ToList();



            var model = new UserViewModel { allUsers = uzytkownicyVM, Uczestnicy = uczestnikVM, Trenerzy = trenerVM, Kierownicy = kierownikVM, noRolesUsers = noRolesUsersVM };

            if (ModelState.IsValid)
            {
                db.Szkolenie.Add(szkolenie);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.IdWojewodztwa = new SelectList(db.Wojewodztwo, "Id", "Nazwa");
            ViewBag.IdKierownika = new SelectList(model.Kierownicy, "Id", "Nazwa", szkolenie.IdKierownika);
            ViewBag.IdTrenera = new SelectList(model.Trenerzy, "Id", "Nazwa", szkolenie.IdTrenera);
            ViewBag.IdFinansowania = new SelectList(db.Finansowanie, "Id", "Nazwa", szkolenie.IdFinansowania);
            ViewBag.IdKategorii = new SelectList(db.Kategoria, "Id", "Nazwa", szkolenie.IdKategorii);
            ViewBag.IdMiejscowosc = new SelectList(db.Miejscowosc, "Id", "Nazwa", szkolenie.IdMiejscowosc);
            ViewBag.IdNazwaSzkolenia = new SelectList(db.NazwaSzkolenia, "Id", "Nazwa", szkolenie.IdNazwaSzkolenia);
            return View(szkolenie);
        }

        // GET: Szkolenie/Edit/5
        [Authorize(Roles = "Kierownik")]
        public ActionResult Edit(int? id)
        {
            var allusers = context.Users.ToList();

            var noRolesUsers = allusers.Where(x => !x.Roles.Select(role => role.RoleId).Contains(1)).ToList();
            noRolesUsers = noRolesUsers.Where(x => !x.Roles.Select(role => role.RoleId).Contains(2)).ToList();
            noRolesUsers = noRolesUsers.Where(x => !x.Roles.Select(role => role.RoleId).Contains(3)).ToList();
            var noRolesUsersVM = noRolesUsers.Select(user => new UserViewModel { Email = user.Email }).ToList();

            var uzytkownicyVM = allusers.Select(user => new UserViewModel { Id = user.Id, Imie = user.Imie, Nazwisko = user.Nazwisko, Nazwa = user.Imie + " " + user.Nazwisko, Email = user.Email }).ToList();

            var uczestnik = allusers.Where(x => x.Roles.Select(role => role.RoleId).Contains(3)).ToList();
            var uczestnikVM = uczestnik.Select(user => new UserViewModel { Id = user.Id, Imie = user.Imie, Nazwisko = user.Nazwisko, Nazwa = user.Imie + " " + user.Nazwisko, Email = user.Email }).ToList();

            var trener = allusers.Where(x => x.Roles.Select(role => role.RoleId).Contains(2)).ToList();
            var trenerVM = trener.Select(user => new UserViewModel { Id = user.Id, Imie = user.Imie, Nazwisko = user.Nazwisko, Nazwa = user.Imie + " " + user.Nazwisko, Email = user.Email }).ToList();

            var kierownik = allusers.Where(x => x.Roles.Select(role => role.RoleId).Contains(1)).ToList();
            var kierownikVM = kierownik.Select(user => new UserViewModel { Id = user.Id, Imie = user.Imie, Nazwisko = user.Nazwisko, Nazwa = user.Imie + " " + user.Nazwisko, Email = user.Email }).ToList();



            var model = new UserViewModel { allUsers = uzytkownicyVM, Uczestnicy = uczestnikVM, Trenerzy = trenerVM, Kierownicy = kierownikVM, noRolesUsers = noRolesUsersVM };


            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Szkolenie szkolenie = db.Szkolenie.Find(id);
            if (szkolenie == null)
            {
                return HttpNotFound();
            }
            ViewBag.IdKierownika = new SelectList(model.Kierownicy, "Id", "Nazwa", szkolenie.IdKierownika);
            ViewBag.IdTrenera = new SelectList(model.Trenerzy, "Id", "Nazwa", szkolenie.IdTrenera);
            ViewBag.IdFinansowania = new SelectList(db.Finansowanie, "Id", "Nazwa", szkolenie.IdFinansowania);
            ViewBag.IdKategorii = new SelectList(db.Kategoria, "Id", "Nazwa", szkolenie.IdKategorii);
            ViewBag.IdMiejscowosc = new SelectList(db.Miejscowosc, "Id", "Nazwa", szkolenie.IdMiejscowosc);
            ViewBag.IdNazwaSzkolenia = new SelectList(db.NazwaSzkolenia, "Id", "Nazwa", szkolenie.IdNazwaSzkolenia);
            ViewBag.IdWojewodztwa = new SelectList(db.Wojewodztwo, "Id", "Nazwa", szkolenie.IdWojewodztwa);

            return View(szkolenie);
        }

        // POST: Szkolenie/Edit/5
        // Aby zapewnić ochronę przed atakami polegającymi na przesyłaniu dodatkowych danych, włącz określone właściwości, z którymi chcesz utworzyć powiązania.
        // Aby uzyskać więcej szczegółów, zobacz https://go.microsoft.com/fwlink/?LinkId=317598.
        [Authorize(Roles = "Kierownik")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,IdNazwaSzkolenia,IdWojewodztwa,IdKategorii,IdKierownika,IdTrenera,LiczbaGodzin,DataOd,DataDo,KodPocztowy,IdMiejscowosc,Ulica,IdFinansowania,Koszt,CzyWydanoZaswiadczenie,CzyZrealizowano,KodSzkolenia,CzyOtwarte,CzyWidoczne,Uwagi")] Szkolenie szkolenie)
        {
               var allusers = context.Users.ToList();

            var noRolesUsers = allusers.Where(x => !x.Roles.Select(role => role.RoleId).Contains(1)).ToList();
            noRolesUsers = noRolesUsers.Where(x => !x.Roles.Select(role => role.RoleId).Contains(2)).ToList();
            noRolesUsers = noRolesUsers.Where(x => !x.Roles.Select(role => role.RoleId).Contains(3)).ToList();
            var noRolesUsersVM = noRolesUsers.Select(user => new UserViewModel { Email = user.Email }).ToList();

            var uzytkownicyVM = allusers.Select(user => new UserViewModel { Id = user.Id, Imie = user.Imie, Nazwisko = user.Nazwisko, Nazwa = user.Imie + " " + user.Nazwisko, Email = user.Email }).ToList();

            var uczestnik = allusers.Where(x => x.Roles.Select(role => role.RoleId).Contains(3)).ToList();
            var uczestnikVM = uczestnik.Select(user => new UserViewModel { Id = user.Id, Imie = user.Imie, Nazwisko = user.Nazwisko, Nazwa = user.Imie + " " + user.Nazwisko, Email = user.Email }).ToList();

            var trener = allusers.Where(x => x.Roles.Select(role => role.RoleId).Contains(2)).ToList();
            var trenerVM = trener.Select(user => new UserViewModel { Id = user.Id, Imie = user.Imie, Nazwisko = user.Nazwisko, Nazwa = user.Imie + " " + user.Nazwisko, Email = user.Email }).ToList();

            var kierownik = allusers.Where(x => x.Roles.Select(role => role.RoleId).Contains(1)).ToList();
            var kierownikVM = kierownik.Select(user => new UserViewModel { Id = user.Id, Imie = user.Imie, Nazwisko = user.Nazwisko, Nazwa = user.Imie + " " + user.Nazwisko, Email = user.Email }).ToList();



            var model = new UserViewModel { allUsers = uzytkownicyVM, Uczestnicy = uczestnikVM, Trenerzy = trenerVM, Kierownicy = kierownikVM, noRolesUsers = noRolesUsersVM };
            
            if (ModelState.IsValid)
            {
                db.Entry(szkolenie).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.IdKierownika = new SelectList(model.Kierownicy, "Id", "Nazwa", szkolenie.IdKierownika);
            ViewBag.IdTrenera = new SelectList(model.Trenerzy, "Id", "Nazwa", szkolenie.IdTrenera);
            ViewBag.IdFinansowania = new SelectList(db.Finansowanie, "Id", "Nazwa", szkolenie.IdFinansowania);
            ViewBag.IdKategorii = new SelectList(db.Kategoria, "Id", "Nazwa", szkolenie.IdKategorii);
            ViewBag.IdMiejscowosc = new SelectList(db.Miejscowosc, "Id", "Nazwa", szkolenie.IdMiejscowosc);
            ViewBag.IdNazwaSzkolenia = new SelectList(db.NazwaSzkolenia, "Id", "Nazwa", szkolenie.IdNazwaSzkolenia);
            ViewBag.IdWojewodztwa = new SelectList(db.Wojewodztwo, "Id", "Nazwa", szkolenie.IdWojewodztwa);

            return View(szkolenie);
        }
        [Authorize(Roles = "Kierownik")]
        // GET: Szkolenie/Delete/5
        public ActionResult Delete(int? id)
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
            return View(szkolenie);
        }
        [Authorize(Roles = "Kierownik")]
        // POST: Szkolenie/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Szkolenie szkolenie = db.Szkolenie.Find(id);
            db.Szkolenie.Remove(szkolenie);
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
