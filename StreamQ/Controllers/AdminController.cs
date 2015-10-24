using StreamQ.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace StreamQ.Controllers
{
    [Authorize(Users ="iain@norman.org")]
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
    }
}