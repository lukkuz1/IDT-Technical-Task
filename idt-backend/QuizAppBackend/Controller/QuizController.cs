using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using QuizAppBackend.Data;
using QuizAppBackend.Models;
using System.Linq;
using System.Threading.Tasks;

namespace QuizAppBackend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class QuizController : ControllerBase
    {
        private readonly QuizContext _context;

        public QuizController(QuizContext context)
        {
            _context = context;
        }

        // GET: api/quiz
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Quiz>>> GetQuizzes()
        {
            var quizzes = await _context.Quizzes
                .Include(q => q.Options)
                .ToListAsync();
            return Ok(quizzes);
        }

        // POST: api/quiz
        [HttpPost]
        public async Task<ActionResult<Answer>> SubmitAnswer(Answer answer)
        {
            if (answer == null || string.IsNullOrEmpty(answer.Email))
            {
                return BadRequest("Invalid answer or missing email.");
            }

            if (answer.SelectedOptions == null || !answer.SelectedOptions.Any())
            {
                return BadRequest("No options selected.");
            }


            var firstOption = await _context.Options
                .AsNoTracking()  // Prevents tracking of this entity initially
                .Where(o => o.Id == answer.SelectedOptions.First().Id)
                .FirstOrDefaultAsync();

            if (firstOption == null)
            {
                return BadRequest("Selected option not found.");
            }

            _context.Attach(firstOption);


            var quiz = await _context.Quizzes
                .Include(q => q.Options)
                .FirstOrDefaultAsync(q => q.Id == firstOption.QuizId);

            if (quiz == null)
            {
                return NotFound("Quiz not found.");
            }


            answer.Score = CalculateScore(answer, quiz);

            _context.Answers.Add(answer);
            await _context.SaveChangesAsync();

            await UpdateHighScores(answer);


            return Ok(answer);
        }

        private int CalculateScore(Answer answer, Quiz quiz)
        {
            int score = 0;

            switch (quiz.Type)
            {
                case QuestionType.Radio:
                    if (answer.SelectedOptions.Count == 1 && quiz.Options.Any(o => o.Id == answer.SelectedOptions[0].Id && o.IsCorrect))
                    {
                        score += 100;
                    }
                    break;

                case QuestionType.Checkbox:
                    int correctCount = quiz.Options.Count(o => o.IsCorrect);
                    int selectedCorrect = answer.SelectedOptions.Count(o => quiz.Options.Any(option => option.Id == o.Id && option.IsCorrect));
                    if (correctCount > 0)
                    {
                        score += 100 / correctCount * selectedCorrect;
                    }
                    break;

                case QuestionType.Textbox:
                    if (!string.IsNullOrWhiteSpace(answer.TextAnswer) && string.Equals(answer.TextAnswer.Trim(), quiz.CorrectAnswer, StringComparison.OrdinalIgnoreCase))
                    {
                        score += 100;
                    }
                    break;
            }

            return score;
        }


        private async Task UpdateHighScores(Answer answer)
        {
            var highScores = await _context.HighScores.OrderByDescending(h => h.Score).ToListAsync();

            if (highScores.Count < 10 || answer.Score > highScores.Last().Score)
            {
                var newHighScore = new HighScore
                {
                    Email = answer.Email,
                    Score = answer.Score,
                    DateTime = DateTime.UtcNow
                };


                _context.HighScores.Add(newHighScore);


                if (highScores.Count >= 10)
                {
                    var lowestHighScore = highScores.Last();
                    _context.HighScores.Remove(lowestHighScore);
                }


                await _context.SaveChangesAsync();
            }
        }
    }
}