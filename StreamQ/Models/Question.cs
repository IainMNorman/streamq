using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace StreamQ.Models
{
    public class Question
    {
        public int Id { get; set; }
        public string Text { get; set; }
        public bool Rejected { get; set; }
        public bool Answered { get; set; }
        public bool Deleted { get; set; }
        public string Answer { get; set; }
        public DateTime TimeStamp { get; set; }

        public virtual ApplicationUser Questioner { get; set; }

        [InverseProperty("Question")]
        public virtual ICollection<Vote> Votes { get; set; }

        [NotMapped]
        public int TotalVotes
        {
            get
            {
                return this.Votes.Where(v => v.Active).Sum(s => s.VoteValue);
            }
        }

    }

}