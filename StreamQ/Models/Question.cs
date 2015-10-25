using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace StreamQ.Models
{
    public class Question
    {
        public int Id { get; set; }
        public string Text { get; set; }
        public int UpVotes { get; set; }
        public int DownVotes { get; set; }
        public bool Rejected { get; set; }
        public bool Answered { get; set; }
        public string OwnerId { get; set; }

    }
}