namespace QuizAppBackend.Models
{
    public class Answer
    {
        public int Id { get; set; }
        public int QuizId { get; set; }
        public string Email { get; set; }
        public List<Option> SelectedOptions { get; set; } // For multiple-choice questions
        public string TextAnswer { get; set; } // For text input questions
        public int Score { get; set; } // Calculated score for the answer
    }
}
