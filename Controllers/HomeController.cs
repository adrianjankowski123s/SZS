using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using SZS.Models;

namespace SZS.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Kontakt()
        {
           
            return View();
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Kontakt(ContactModels model)
        {
            if (ModelState.IsValid)
            {
                var mail = new MailMessage();
                mail.To.Add(new MailAddress(model.SenderEmail));
                mail.To.Add(new MailAddress("szsszkolenia@gmail.com"));
                mail.Subject = "Temat twojego maila";
                mail.Body = string.Format("<p>Email od: {0} ({1})</p><p>Wiadomość:</p><p>{2} {3}</p>", model.SenderName, model.SenderEmail, model.Topic, model.Message);
                mail.IsBodyHtml = true;
                using (var smtp = new SmtpClient())
                {
                    await smtp.SendMailAsync(mail);
                    return RedirectToAction("SuccessMessage");
                }
            }
            return View(model);
        }
        public ActionResult Contact(int id)
        {
            SZSModel db = new SZSModel();
            Models.SZSModel ctx = new Models.SZSModel();

            Szkolenie szkolenie = db.Szkolenie.Find(id);
            ViewBag.IdS = szkolenie.Id;
            ViewBag.NazwaS = szkolenie.NazwaSzkolenia.Nazwa;
            ViewBag.SenderName = User.Identity.Name;
            var users = ctx.Users.ToList();
            foreach (var user in users)
            {
                var nazwa = user.Imie + " " + user.Nazwisko;
                if (nazwa.ToLower() == User.Identity.Name.ToString().ToLower())
                {
                    var model = user;
                    ViewBag.SenderEmail = model.Email;
                }
            }

            return View();
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Contact(ContactModels model, string Topic)
        {
            if (ModelState.IsValid)
            {
                model.Topic = Topic;
                var mail = new MailMessage();
                mail.To.Add(new MailAddress(model.SenderEmail));
                mail.To.Add(new MailAddress("szsszkolenia@gmail.com"));
                mail.Subject = "Temat twojego maila";
                mail.Body = string.Format("<p>Email od: {0} ({1})</p><p>Wiadomość:</p><p>{2} {3}</p>", model.SenderName, model.SenderEmail, model.Topic, model.Message);
                mail.IsBodyHtml = true;
                using (var smtp = new SmtpClient())
                {
                    await smtp.SendMailAsync(mail);
                    return RedirectToAction("SuccessMessage");
                }
            }
            return View(model);
        }

        public ActionResult SuccessMessage()
        {
            return View();
        }


    }
}