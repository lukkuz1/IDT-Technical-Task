namespace QuizAppBackend.Models
{
    public class HighScore
    {
        public int Position { get; set; }
        public string Email { get; set; }
        public int Score { get; set; }
        public DateTime DateTime { get; set; }
    }
}
