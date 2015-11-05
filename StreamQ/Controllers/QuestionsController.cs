using Microsoft.AspNet.Identity;
using StreamQ.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Results;

namespace StreamQ.Controllers
{
    [RoutePrefix("api/v1")]
    public class QuestionsController : ApiController
    {

        ApplicationDbContext db = new ApplicationDbContext();

        [HttpGet]
        [Route("questions/{qid}/{voteType}")]
        public HttpResponseMessage Questions(int qid, VoteType voteType)
        {

            if (User.Identity.IsAuthenticated == false)
            {
                return new HttpResponseMessage(HttpStatusCode.Forbidden);
            }
            
            var user = db.Users.Find(User.Identity.GetUserId());

            var q = db.Questions.Find(qid);

            if (q == null)
            {
                return new HttpResponseMessage(HttpStatusCode.NotFound);
            }

            var existingVote = db.Votes


        }

    }

    public enum VoteType
    {
        Remove = 0,
        Up,
        Down
    }
}