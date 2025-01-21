using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace QuizAppBackend.Models
{
    public class Quiz
    {
        [Key]
        public int Id { get; set; }
        public string Question { get; set; }
        public QuestionType Type { get; set; }
        public List<Option> Options { get; set; }
        public string? CorrectAnswer { get; set; }
    }

    public enum QuestionType
    {
        Radio,
        Checkbox,
        Textbox
    }
}