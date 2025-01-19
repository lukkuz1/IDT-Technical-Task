using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace QuizAppBackend.Models
{
    public class Answer
    {
        [Key]
        public int Id { get; set; }

        [ForeignKey("Quiz")]
        public int QuizId { get; set; }
        public Quiz Quiz { get; set; }

        public string Email { get; set; }
        public List<Option> SelectedOptions { get; set; }
        public string TextAnswer { get; set; }
        public int Score { get; set; }
    }
}
