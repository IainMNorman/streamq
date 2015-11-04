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
            }
            model.Questions = db.Questions
                .OrderBy(o => o.Answered)
                .ThenByDescending(o => o.Votes.Sum(v => v.VoteValue));
            model.QuestionText = null;
            return View(model);
        }

        [HttpPost]
        [Authorize]
        public ActionResult AskQuestion(QuestionsVM model)
        {
            model.Questions = db.Questions.OrderByDescending(o => o.Votes.Sum(v => v.VoteValue));

            if (ModelState.IsValid)
            {

                var user = db.Users.Find(User.Identity.GetUserId());

                db.Questions.Add(new Question()
                {
                    Text = model.QuestionText,
                    Questioner = user,
                    TimeStamp = DateTime.UtcNow
                });
                db.SaveChanges();
                return RedirectToAction("Index");

            }
            return View("Index", model);
        }

    }
}