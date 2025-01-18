namespace QuizAppBackend.Models
{
    public class Quiz
    {
        public int Id { get; set; }
        public string Question { get; set; }
        public QuestionType Type { get; set; } // Enum for question type: Radio, Checkbox, or Textbox
        public List<Option> Options { get; set; } // For multiple choice questions
    }

    public enum QuestionType
    {
        Radio,
        Checkbox,
        Textbox
    }
}
