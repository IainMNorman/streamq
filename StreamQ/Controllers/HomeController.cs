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
            model.Questions = GetQuestions();
            model.QuestionText = null;
            return View(model);
        }



        [HttpPost]
        [Authorize]
        public ActionResult AskQuestion(QuestionsVM model)
        {

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

        private List<QuestionVM> GetQuestions()
        {
            var qs = new List<QuestionVM>();


            qs = db.Questions.Where(q => q.Deleted != true).Select(s => new QuestionVM
            {
                Id = s.Id,
                Text = s.Text,                
                TotalVotes = s.Votes.Where(v => v.Active == true).Sum(v => (int?)v.VoteValue) ?? 0,
                CurrentUserVoteValue = 0,
                Answered = s.Answered
            }).OrderBy(o => o.TotalVotes).ToList();


            if (User.Identity.IsAuthenticated)
            {
                var user = db.Users.Find(User.Identity.GetUserId());
                foreach (var vote in user.MyVotes.Where(v => !v.Question.Deleted))
                {
                    
                    qs.FirstOrDefault(q => q.Id == vote.Question.Id).CurrentUserVoteValue = vote.VoteValue;
                }
            }

            return qs;
        }

    }
}