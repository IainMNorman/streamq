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
        [MaxLength(140, ErrorMessage = "Question is too long. 140 characters maximum.")]
        public string QuestionText { get; set; }
    }
}