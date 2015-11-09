using StreamQ.Models;
using StreamQ.ViewModels;
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
            var qs = new List<QuestionVM>();

            qs = db.Questions.Where(q => q.Deleted != true && q.Answered == false && q.Rejected == false).Select(s => new QuestionVM
            {
                Id = s.Id,
                Text = s.Text,
                Questioner = s.Questioner.UserName,
                TotalVotes = s.Votes.Where(v => v.Active == true).Sum(v => (int?)v.VoteValue) ?? 0,
                CurrentUserVoteValue = 0
            }).OrderByDescending(o => o.TotalVotes).ToList().ToList();

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

        public ActionResult Delete(int id)
        {
            var q = db.Questions.Find(id);
            q.Deleted = true;
            db.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}