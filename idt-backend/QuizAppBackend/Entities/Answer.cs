using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace QuizAppBackend.Models
{
    public class Answer
    {
        [Key]
        public int Id { get; set; }

        public string Email { get; set; }
        public List<Option> SelectedOptions { get; set; } = new List<Option>();
        public string? TextAnswer { get; set; }
        public int Score { get; set; }
    }
}