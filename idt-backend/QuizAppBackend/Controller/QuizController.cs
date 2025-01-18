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
            return await _context.Quizzes.ToListAsync();
        }

        // POST: api/quiz/submit
        [HttpPost("submit")]
        public async Task<ActionResult<Answer>> SubmitAnswer(Answer answer)
        {
            // Calculate score based on rules
            answer.Score = CalculateScore(answer);

            // Save answer to database
            _context.Answers.Add(answer);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetQuizzes), new { id = answer.Id }, answer);
        }

        // Helper method to calculate the score
        private int CalculateScore(Answer answer)
        {
            int score = 0;

            // Logic to calculate score based on question type
            // Example: Radio buttons = 100 points for correct answer, etc.

            return score;
        }
    }
}
