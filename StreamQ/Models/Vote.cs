using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace StreamQ.Models
{
    public class Vote
    {
        public int Id { get; set; }
        public int VoteValue { get; set; }
        public DateTime TimeStamp { get; set; }

        public ApplicationUser Voter { get; set; }
    }
}