using StreamQ.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace StreamQ.ViewModels
{
    public class QuestionsVM
    {
        public IEnumerable<Question> Questions { get; set; }
        [Required(ErrorMessage = "You must provide a question.")]
        [MinLength(10, ErrorMessage = "Question is too short.")]
        public string QuestionText { get; set; }
        public int VoteQuota { get; set; }
        public int QuestionQuota { get; set; }
    }
}