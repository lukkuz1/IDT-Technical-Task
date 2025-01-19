using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using QuizAppBackend.Data;
using QuizAppBackend.Models;

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
            return await _context.Quizzes.Include(q => q.Options).ToListAsync();
        }

        // GET: api/quiz/{id}
        [HttpGet("{id}")]
        public async Task<ActionResult<Quiz>> GetQuiz(int id)
        {
            var quiz = await _context.Quizzes.Include(q => q.Options).FirstOrDefaultAsync(q => q.Id == id);

            if (quiz == null)
            {
                return NotFound();
            }

            return quiz;
        }

        // POST: api/quiz/submit
        [HttpPost("submit")]
        public async Task<ActionResult<Answer>> SubmitAnswer(Answer answer)
        {
            if (answer == null || string.IsNullOrEmpty(answer.Email))
            {
                return BadRequest("Invalid answer or email.");
            }
            answer.Score = CalculateScore(answer);
            _context.Answers.Add(answer);
            await _context.SaveChangesAsync();
            await UpdateHighScores(answer);
            return CreatedAtAction(nameof(GetQuizzes), new { id = answer.Id }, answer);
        }

        private int CalculateScore(Answer answer)
        {
            int score = 0;

            var quiz = _context.Quizzes.Include(q => q.Options).FirstOrDefault(q => q.Id == answer.QuizId);

            if (quiz == null)
            {
                return score;
            }

            switch (quiz.Type)
            {
                case QuestionType.Radio:
                    if (answer.SelectedOptions.Count == 1 && quiz.Options.FirstOrDefault(o => o.Id == answer.SelectedOptions[0].Id)?.IsCorrect == true)
                    {
                        score += 100;
                    }
                    break;

                case QuestionType.Checkbox:
                    int correctCount = quiz.Options.Count(o => o.IsCorrect);
                    int selectedCorrect = answer.SelectedOptions.Count(o => quiz.Options.FirstOrDefault(option => option.Id == o.Id)?.IsCorrect == true);
                    if (correctCount > 0)
                    {
                        score += (100 / correctCount) * selectedCorrect;
                    }
                    break;

                case QuestionType.Textbox:
                    if (!string.IsNullOrEmpty(answer.TextAnswer) && string.Equals(answer.TextAnswer, quiz.Question, StringComparison.OrdinalIgnoreCase))
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
