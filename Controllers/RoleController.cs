using AutoMapper;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using SZS.Models;

namespace SZS.Controllers
{
    [Authorize(Roles = "Kierownik")]
    public class RoleController : Controller
    {
        Models.SZSModel db = new Models.SZSModel();
        // GET: Role
        public ActionResult Index()
        {
            
            var Roles = db.Roles.ToList();
            return View(Roles);
        }

        // GET: /Roles/Create
        public ActionResult Create()
        {
            return View();
        }

        //
        // POST: /Roles/Create


        [HttpPost]
        public async Task<ActionResult> Create(RoleViewModel roleViewModel)
        {
            if (ModelState.IsValid)
            {
                
                var RoleManager = new ApplicationRoleManager(new CustomRoleStore(db));

                var role = new CustomRole(roleViewModel.Name);
                var roleresult = await RoleManager.CreateAsync(role);
              
                if (!roleresult.Succeeded)
                {
                    ModelState.AddModelError("", roleresult.Errors.First());
                    return View();
                }
                return RedirectToAction("Create");
            }
            return View();
        }



    }
}



