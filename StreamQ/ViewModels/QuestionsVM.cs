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
        public IEnumerable<QuestionVM> Questions { get; set; }
        [Required(ErrorMessage = "You must provide a question.")]
        [MinLength(10, ErrorMessage = "Question is too short.")]
        [MaxLength(140, ErrorMessage = "Question is too long. 140 characters maximum.")]
        public string QuestionText { get; set; }
    }

    public class QuestionVM
    {
        public int Id { get; set; }
        public string Text { get; set; }
        public int TotalVotes { get; set; }
        public int CurrentUserVoteValue { get; set; }
        public bool Answered { get; set; }
        public string Questioner { get; set; }
    }
}