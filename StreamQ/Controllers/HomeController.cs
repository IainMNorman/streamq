using StreamQ.Models;
using StreamQ.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;

namespace StreamQ.Controllers
{
    public class HomeController : Controller
    {

        ApplicationDbContext db = new ApplicationDbContext();


        public ActionResult Index()
        {
            var model = new QuestionsVM();
            if (User.Identity.IsAuthenticated)
            {
                var user = db.Users.Find(User.Identity.GetUserId());
                model.VoteQuota = user.VoteQuota;
                model.QuestionQuota = user.QuestionQuota;
            }
            model.Questions = db.Questions.OrderByDescending(o => o.UpVotes - o.DownVotes);
            model.QuestionText = null;
            return View(model);
        }

        [HttpPost]
        [Authorize]
        public ActionResult Index(QuestionsVM model)
        {
            model.Questions = db.Questions.OrderByDescending(o => o.UpVotes - o.DownVotes);

            if (ModelState.IsValid)
            {

                var user = db.Users.Find(User.Identity.GetUserId());
                model.VoteQuota = user.VoteQuota;
                model.QuestionQuota = user.QuestionQuota;
                if (user.QuestionQuota > 0)
                {
                    user.QuestionQuota--;
                    db.Questions.Add(new Question() { Text = model.QuestionText, UpVotes = 1, OwnerId = user.Id });
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
                else
                {
                    ModelState.AddModelError("QuestionText", "Out of questions!");
                    return View("Index", model);
                }
            }
            return View("Index", model);
        }

        [HttpGet]
        [Authorize]
        public ActionResult VoteUp(int id)
        {
            Vote(id, true);
            return RedirectToAction("Index");
        }

        [HttpGet]
        [Authorize]
        public ActionResult VoteDown(int id)
        {
            Vote(id, false);
            return RedirectToAction("Index");
        }

        private void Vote(int id, bool up)
        {
            var user = db.Users.Find(User.Identity.GetUserId());
            var question = db.Questions.Find(id);

            if (user.VoteQuota > 0 && user.Id != question.OwnerId)
            {

                user.VoteQuota--;
                if (up)
                {
                    question.UpVotes++;
                }
                else
                {
                    question.DownVotes++;
                }
                db.SaveChanges();
            }
        }

    }
}