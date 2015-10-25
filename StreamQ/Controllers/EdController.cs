using StreamQ.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;


namespace StreamQ.Controllers
{
    [Authorize(Roles = "Administrator")]
    public class EdController : Controller
    {
        ApplicationDbContext db = new ApplicationDbContext();
        // GET: Ed
        public ActionResult Index()
        {
            var qs = db.Questions.Where(q => q.Rejected != true && q.Answered != true)
                .OrderByDescending(o => o.UpVotes - o.DownVotes);
            return View(qs);
        }

        public ActionResult Answer(int id)
        {
            var q = db.Questions.Find(id);
            q.Answered = true;
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        public ActionResult Reject(int id)
        {
            var q = db.Questions.Find(id);
            q.Rejected = true;
            db.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}