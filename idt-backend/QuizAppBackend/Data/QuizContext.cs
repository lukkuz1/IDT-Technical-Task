using Microsoft.EntityFrameworkCore;
using QuizAppBackend.Models;

namespace QuizAppBackend.Data
{
    public class QuizContext : DbContext
    {
        public QuizContext(DbContextOptions<QuizContext> options)
            : base(options)
        { }

        public DbSet<Quiz> Quizzes { get; set; }
        public DbSet<Answer> Answers { get; set; }
        public DbSet<HighScore> HighScores { get; set; }
        public DbSet<Option> Options { get; set; }
    }
}