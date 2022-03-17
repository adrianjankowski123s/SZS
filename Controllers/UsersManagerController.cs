using Microsoft.AspNet.Identity.Owin;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SZS.Models;
using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity.EntityFramework;

namespace SZS.Controllers
{
    [Authorize(Roles = "Kierownik,Trener")]
    public class UsersManagerController : Controller
    {
        private SZSModel ctx = new SZSModel();
        private Models.SZSModel db = new Models.SZSModel();
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



        // GET: UsersManager
        public ActionResult Index()
        {
           
       
            var allusers = db.Users.ToList();
            
            var noRolesUsers = allusers.Where(x => !x.Roles.Select(role => role.RoleId).Contains(1)).ToList();
            noRolesUsers = noRolesUsers.Where(x => !x.Roles.Select(role => role.RoleId).Contains(2)).ToList();
            noRolesUsers = noRolesUsers.Where(x => !x.Roles.Select(role => role.RoleId).Contains(3)).ToList();
            var noRolesUsersVM = noRolesUsers.Select(user => new UserViewModel { Imie = user.Imie, Nazwisko = user.Nazwisko, Email = user.Email }).ToList();

            var uzytkownicyVM = allusers.Select(user => new UserViewModel { Imie = user.Imie, Nazwisko = user.Nazwisko, Email = user.Email }).ToList();

            var uczestnik = allusers.Where(x => x.Roles.Select(role => role.RoleId).Contains(3)).ToList();
            var uczestnikVM = uczestnik.Select(user => new UserViewModel { Imie = user.Imie, Nazwisko = user.Nazwisko, Email = user.Email }).ToList();

            var trener = allusers.Where(x => x.Roles.Select(role => role.RoleId).Contains(2)).ToList();
            var trenerVM = trener.Select(user => new UserViewModel { Imie = user.Imie, Nazwisko = user.Nazwisko, Email = user.Email }).ToList();

            var kierownik = allusers.Where(x => x.Roles.Select(role => role.RoleId).Contains(1)).ToList();
            var kierownikVM = kierownik.Select(user => new UserViewModel { Imie = user.Imie, Nazwisko = user.Nazwisko, Email = user.Email }).ToList();



            var model = new UserViewModel {allUsers = uzytkownicyVM, Uczestnicy = uczestnikVM, Trenerzy = trenerVM, Kierownicy = kierownikVM, noRolesUsers = noRolesUsersVM };

            return View(model);
        }

        [HttpGet]
        public ActionResult ChangeRole(string Username)
        {
            if (Username == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            UserViewModel UVM = new UserViewModel();
            UVM.Email = Username;
            TempData["Username"] = Username;
            return View(UVM);
        }


        [HttpPost]
        public ActionResult ChangeRole(UserViewModel UVM)
        {
            var Username = TempData["Username"].ToString();
            string SelectedValue = UVM.SelectedRole;
            if (Username != null)
            {
                var user = UserManager.FindByEmail(Username);
                var id = user.Id;
                if (UserManager.GetRoles(user.Id).FirstOrDefault() != null)
                {
                    string ActualRole = UserManager.GetRoles(user.Id).FirstOrDefault().ToString();

                    if (ActualRole == "Kierownik")
                    {
                        UserManager.RemoveFromRole(id, "Kierownik");
                        if (SelectedValue != null)
                        {
                            UserManager.AddToRole(id, SelectedValue);
                        }

                    }
                    if (ActualRole == "Trener")
                    {
                        UserManager.RemoveFromRole(id, "Trener");
                        if (SelectedValue != null)
                        {
                            UserManager.AddToRole(id, SelectedValue);
                        }
                    }
                    if (ActualRole == "Uczestnik")
                    {
                        UserManager.RemoveFromRole(id, "Uczestnik");
                        if (SelectedValue != null)
                        {
                            UserManager.AddToRole(id, SelectedValue);
                        }
                    }
                }
                else
                {
                    if (SelectedValue != null)
                    {
                        UserManager.AddToRole(id, SelectedValue);
                    }
                }

            }

            return RedirectToAction("Index");
        }

        // GET: UsersManager/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: UsersManager/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: UsersManager/Create
        [HttpPost]
        public ActionResult Create(FormCollection collection)
        {
            try
            {
                // TODO: Add insert logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        public ActionResult EditUser(string email)
        {
            ViewBag.PracownikUmiejetnosc = new SelectList(ctx.Umiejetnosci, "Id", "Nazwa");
            ViewBag.Miejscowosc = new SelectList(ctx.Miejscowosc, "Id", "Nazwa");
            ViewBag.Miejscowosc1 = new SelectList(ctx.Miejscowosc, "Id", "Nazwa");
            ViewBag.Wyksztalcenie = new SelectList(ctx.Wyksztalcenie, "Id", "Nazwa");
            if (email == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ApplicationUser appUser = new ApplicationUser();
            appUser = UserManager.FindByEmail(email);

            if (appUser == null)
            {
                return HttpNotFound();
            }
            UserEdit user = new UserEdit();
            user.UserName = appUser.UserName;
            user.Imie = appUser.Imie;
            user.Nazwisko = appUser.Nazwisko;
            user.Email = appUser.Email;
            user.Plec = appUser.Plec;
            user.DataUrodzenia = appUser.DataUrodzenia;
            user.IDMiejsceUrodzenia = appUser.IDMiejsceUrodzenia;
            user.Pesel = appUser.Pesel;
            user.Ulica = appUser.Ulica;
            user.IDMiejscowosc = appUser.IDMiejscowosc;
            user.KodPocztowy = appUser.KodPocztowy;
            user.IDWyksztalcenia = appUser.IDWyksztalcenia;
            user.Telefon = appUser.Telefon;
           
            return View(user);
        }


        [HttpPost]
        public async Task<ActionResult> EditUser(UserEdit model)
        {
            ViewBag.PracownikUmiejetnosc = new SelectList(ctx.Umiejetnosci, "Id", "Nazwa");
            ViewBag.Miejscowosc = new SelectList(ctx.Miejscowosc, "Id", "Nazwa");
            ViewBag.Miejscowosc1 = new SelectList(ctx.Miejscowosc, "Id", "Nazwa");
            ViewBag.Wyksztalcenie = new SelectList(ctx.Wyksztalcenie, "Id", "Nazwa");


            model.UserName = model.Imie + " " + model.Nazwisko;
                       
            var currentUser = UserManager.FindByEmail(model.Email);
            currentUser.UserName = model.UserName;
            currentUser.Imie = model.Imie;
            currentUser.Nazwisko = model.Nazwisko;
            currentUser.Email = model.Email;
            currentUser.Plec = model.Plec;
            currentUser.DataUrodzenia = model.DataUrodzenia;
            currentUser.IDMiejsceUrodzenia = model.IDMiejsceUrodzenia;
            currentUser.Pesel = model.Pesel;
            currentUser.Ulica = model.Ulica;
            currentUser.IDMiejscowosc = model.IDMiejscowosc;
            currentUser.KodPocztowy = model.KodPocztowy;
            currentUser.IDWyksztalcenia = model.IDWyksztalcenia;

            currentUser.Telefon = model.Telefon;



            await UserManager.UpdateAsync(currentUser);
            db.SaveChanges();

            return RedirectToAction("Index");
        }



        public ActionResult DeleteUser(string email)
        {
            if (email == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var user = UserManager.FindByEmail(email);
            if (user == null)
            {
                return HttpNotFound();
            }


            return View(user);
        }

        [HttpPost, ActionName("DeleteUser")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteUserConfirmed(string email)
        {
            var user = await UserManager.FindByEmailAsync(email);

            var result = await UserManager.DeleteAsync(user);
            db.SaveChanges();
            return RedirectToAction("Index");
        }




        // GET: UsersManager/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: UsersManager/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}
