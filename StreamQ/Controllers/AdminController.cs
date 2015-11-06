using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using StreamQ.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace StreamQ.Controllers
{
    [Authorize(Roles="Administrator")]
    public class AdminController : Controller
    {
        ApplicationDbContext db = new ApplicationDbContext();

        // GET: Admin
        public ActionResult Clear()
        {
            db.Database.ExecuteSqlCommand("TRUNCATE TABLE Questions");
            db.Database.ExecuteSqlCommand("UPDATE AspNetUsers SET QuestionQuota = 3, VoteQuota = 10");

            return RedirectToAction("Index", "Home");
        }

        public ActionResult Adminator()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Adminator(string email)
        {
            var roleManager = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>(db));
            roleManager.Create(new IdentityRole("Administrator"));

            var userManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(db));
            var user = userManager.FindByEmail(email);
            userManager.AddToRole(user.Id, "Administrator");

            return View();
        }
    }
}