using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace StreamQ.Models
{
    public class Vote
    {
        public int Id { get; set; }
        public int VoteValue { get; set; }
        public DateTime TimeStamp { get; set; }
        public bool Active { get; set; }

        [InverseProperty("MyVotes")]
        public virtual ApplicationUser Voter { get; set; }

        [InverseProperty("Votes")]
        public virtual Question Question { get; set; }
    }
}