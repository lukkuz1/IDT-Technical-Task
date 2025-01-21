using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace QuizAppBackend.Models
{
    public class HighScore
    {
        [Key]
        public int Id { get; set; }
        public string Email { get; set; }
        public int Score { get; set; }
        public DateTime DateTime { get; set; }
    }
}
