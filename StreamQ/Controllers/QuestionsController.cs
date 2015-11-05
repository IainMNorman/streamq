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

            q.Votes.Where(v => v.Voter == user && v.Active == true).ToList().ForEach(v =>
            {
                v.Active = false;
            });

            int voteValue;

            switch (voteType)
            {
                case VoteType.Remove:
                    voteValue = 0;
                    break;
                case VoteType.Up:
                    voteValue = 1;
                    break;
                case VoteType.Down:
                    voteValue = -1;
                    break;
                default:
                    voteValue = 0;
                    break;
            }

            q.Votes.Add(new Vote()
            {
                Voter = user,
                VoteValue = voteValue,
                TimeStamp = DateTime.UtcNow,
                Active = true        
            });


            try
            {
                db.SaveChangesAsync();
            }
            catch (Exception)
            {
                return new HttpResponseMessage(HttpStatusCode.InternalServerError);
            }

            return new HttpResponseMessage(HttpStatusCode.OK); 

        }

    }

    public enum VoteType
    {
        Remove = 0,
        Up,
        Down
    }
}