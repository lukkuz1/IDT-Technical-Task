using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using QuizAppBackend.Data;
using QuizAppBackend.Models;

namespace QuizAppBackend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class HighScoresController : ControllerBase
    {
        private readonly QuizContext _context;

        public HighScoresController(QuizContext context)
        {
            _context = context;
        }

        // GET: api/highscores
        [HttpGet]
        public async Task<ActionResult<IEnumerable<object>>> GetTopScores()
        {
            var topScores = await _context.HighScores
                .OrderByDescending(h => h.Score)
                .Take(10)
                .ToListAsync();
            var result = topScores.Select((score, index) => new
            {
                Position = index + 1,
                Email = score.Email,
                Score = score.Score,
                DateTime = score.DateTime,
                Color = index == 0 ? "Gold" : index == 1 ? "Silver" : index == 2 ? "Bronze" : null
            });

            return Ok(result);
        }
    }
}
